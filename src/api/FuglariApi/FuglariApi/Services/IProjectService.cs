using FuglariApi.Models;
using FuglariApi.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Services
{
    public interface IProjectService
    {
        public Task CreateProject(CreateProjectRequest request);
        public Task CreateDataset(CreateDatasetRequest request);
        public Task CreateLandmark(CreateLandmarkRequest request);
        public Task<IEnumerable<LandmarkDto>> GetLandmarksForDataset(int datasetId);
        public Task<IEnumerable<Observation>> GetObservationsForLandmark(int landmarkId);
        public Task AddObsevation(PostObservationRequest request);
        public Task<LandmarkDto> GetLandmarkById(int landmarkId);
        public Task<Dataset> GetDatasetById(int observationId);
        public Task<Project> GetProjectById(int projectId);
        public Task<IEnumerable<Project>> ProjectsForUser(string email);
        public Task<IEnumerable<Project>> GetProjectsForUser(string email);
        public Task AddUserToProject(string newPersonEmail, int projectId, string inviterEmail);
        public Task UpdateLandmark(Landmark landmark);
        public Task<IEnumerable<Dataset>> GetDatasetsForProject(int projectId);
        Task<IEnumerable<DatasetDto>> GetDatasetsForPerson(string email);
        public Task<IEnumerable<ProjectDto>> GetProjectDtosForUser(string email);
    }
}
