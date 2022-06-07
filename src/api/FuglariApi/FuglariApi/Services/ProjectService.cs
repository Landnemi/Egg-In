using FuglariApi.Infrastructure;
using FuglariApi.Models;
using FuglariApi.Repositories;
using FuglariApi.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuglariApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUserRepository userRepository;
        private readonly IProjectRepository projectRepository;
        public ProjectService( IUserRepository repository, IProjectRepository _projectRepository)
        {
            userRepository = repository;
            projectRepository = _projectRepository; 
        }

        public async Task AddObsevation(PostObservationRequest request)
        {
            User user = await userRepository.GetUserByEmail(request.Email);
            Landmark landmark = await projectRepository.GetLandmarkById(request.LandMarkId);
            if (landmark == null)
            {
                throw new ErrorCodeException(401, "Landmark", "Landmark does not exist");
            }
            Dataset dataset = await projectRepository.GetDatasetById(landmark.DatasetId);
            if (dataset == null)
            {
                throw new ErrorCodeException(401, "dataset", "dataset does not exist");
            }
            Project project = await projectRepository.GetProjectById(dataset.ProjectId);
            if (project == null)
            {
                throw new ErrorCodeException(401, "Project", "Project does not exist");
            }
            Membership membership = await projectRepository.CheckMembership(user.Id, project.Id);
            if (membership == null)
            {
                throw new ErrorCodeException(401, "DENIED", "You cannot add observations for projects you are not a part of.");
            }
            NewObservation observation = new NewObservation();
            observation.Comment = request.Comment;
            observation.LandmarkId = landmark.Id;
            observation.UserId = user.Id;
            await projectRepository.AddObservationToLandmark(observation);
        }

        public async Task AddUserToProject(string newPersonEmail, int projectId, string inviterEmail)
        {
            User newUser = await userRepository.GetUserByEmail(newPersonEmail);
            if(newUser == null)
            { 
                throw new ErrorCodeException(401, "USER", "User with email " + newPersonEmail + " does not exist.");
            }
            User inviter = await userRepository.GetUserByEmail(inviterEmail);
            if (inviter == null)
            {
                throw new ErrorCodeException(401, "USER", "User with email " + inviterEmail + " does not exist.");
            }
            Project project = await projectRepository.GetProjectById(projectId);
            if (project == null)
            {
                throw new ErrorCodeException(401, "DENIED", "No such project exists");
            }
            Membership inviterMembership = await projectRepository.CheckMembership(inviter.Id, projectId);
            if(inviterMembership == null)
            { 
                throw new ErrorCodeException(401, "DENIED", "You cannot invite people into projects you are not a part of.");
            }
            Membership inviteeMembership = await projectRepository.CheckMembership(newUser.Id, projectId);
            if (inviteeMembership != null)
            {
                throw new ErrorCodeException(401, "DENIED", "You cannot invite people multiple times into projects");
            }
            await projectRepository.AddUserToProject(newUser.Id, project.Id);
        }

        public async Task CreateDataset(CreateDatasetRequest request)
        {
            User user = await userRepository.GetUserByEmail(request.Email);
            if(user == null)
            {
                throw new ErrorCodeException(401, "USER", "User with email " + request.Email + " does not exist.");
            }
            //check membership in project!
            Membership membership = await projectRepository.CheckMembership(user.Id, request.ProjectId);
            if(membership == null) {
                throw new ErrorCodeException(401, "PERMISSIONDENIED", "This user does not have permission to add a dataset to this program.");
            }
            NewDataset ds = new NewDataset();
            ds.ProjectId = request.ProjectId;
            ds.Title = request.Title;
            ds.UserId = user.Id;
            // create dataset...
            await projectRepository.CreateDataset(ds);
            //await projectRepository.CreateDataset();
        }

        public async Task CreateLandmark(CreateLandmarkRequest request)
        {
            User user = await userRepository.GetUserByEmail(request.Email);
            if (user == null)
            {
                throw new ErrorCodeException(401, "USER", "User with email " + request.Email + " does not exist.");
            }
            Dataset dataset = await projectRepository.GetDatasetById(request.DatasetId);
            if (dataset == null)
            {
                throw new ErrorCodeException(404, "DATASET", "This dataset does not exist.");
            }
            Membership membership = await projectRepository.CheckMembership(user.Id, dataset.ProjectId);
            if (membership == null)
            {
                throw new ErrorCodeException(401, "PERMISSIONDENIED", "This user does not have permission to add a landmark to this dataset.");
            }

            NewLandmark newLandmark = new NewLandmark();
            newLandmark.DatasetId = request.DatasetId;
            newLandmark.Status = request.Status;
            newLandmark.Progress = request.Progress;
            newLandmark.Latitude = request.Latitude;
            newLandmark.Longitude = request.Longitude;
           
            await projectRepository.CreateLandmark(newLandmark);
        }

        public async Task CreateProject(CreateProjectRequest request)
        {
            User user = await userRepository.GetUserByEmail(request.Email);
            if(user == null)
            {
                throw new ErrorCodeException(401, "USER", "User with email " + request.Email + " does not exist.");
            }
            await projectRepository.CreateProject(new NewProject(user.Id, request.ProjectTitle));
        }

        public async Task<Dataset> GetDatasetById(int datasetId)
        {
            return await projectRepository.GetDatasetById(datasetId);
        }

        public async Task<IEnumerable<DatasetDto>> GetDatasetsForPerson(string email)
        {
            User user = await userRepository.GetUserByEmail(email);
            if(user == null)
            {
                throw new ErrorCodeException(401, "USER", "User with email " + email + " does not exist.");
            }
            IEnumerable<Project> projects = await GetProjectsForUser(email);
            List<DatasetDto> datasetDtoList = new List<DatasetDto>();
            foreach(Project project in projects)
            {
                IEnumerable<Dataset> _datasets = await projectRepository.GetDatasetsForProject(project.Id);
                foreach (Dataset d in _datasets)
                {
                    IEnumerable<Landmark> l = await projectRepository.GetLandmarksForDataset(d.Id);
                    var temp = new DatasetDto
                    {
                        Landmarks = l,
                        Title = d.Title,
                        Id = d.Id
                    };
                    datasetDtoList.Add(temp);
                }
            }
            return datasetDtoList;
        }

        public async Task<IEnumerable<Dataset>> GetDatasetsForProject(int projectId)
        {
            return await projectRepository.GetDatasetsForProject(projectId);
        }

        public async Task<Landmark> GetLandmarkById(int landmarkId)
        {
            return await projectRepository.GetLandmarkById(landmarkId);
        }

        public async Task<IEnumerable<Landmark>> GetLandmarksForDataset(int datasetId)
        {
            return await projectRepository.GetLandmarksForDataset(datasetId);
        }

        public async Task<IEnumerable<Observation>> GetObservationsForLandmark(int landmarkId)
        {
            return await projectRepository.GetObservationsForLandmark(landmarkId);
        }

        public async Task<Project> GetProjectById(int projectId)
        {
            return await projectRepository.GetProjectById(projectId);
        }

        public async Task<IEnumerable<Project>> GetProjectsForUser(string email)
        {
            User user = await userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new ErrorCodeException(401, "USER", "User with email " + email + " does not exist.");
            }
            return await projectRepository.GetProjectsForUser(user.Id);
        }

        public async Task UpdateLandmark(Landmark landmark)
        {
            await projectRepository.UpdateLandmark(landmark);
        }
    }
}
