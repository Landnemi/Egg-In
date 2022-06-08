using FuglariApi.Models;
using FuglariApi.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Repositories
{
    public interface IProjectRepository
    {
        public Task<Project> GetProjectById(int projectId);
        public Task<IEnumerable<Project>> GetProjectsForUser(int userId);
        public Task<IEnumerable<Dataset>> GetDatasetsForProject(int projectId);
        public Task<IEnumerable<LandmarkDto>> GetLandmarksForDataset(int datasetId);
        public Task AddUserToProject(int projectId, int userId);
        public Task AddObservationToLandmark(NewObservation observation);
        public Task CreateDataset(NewDataset dataset);
        public Task UpdateLandmark(Landmark landmark);
        public Task CreateProject(NewProject request);
        public Task<Membership> CheckMembership(int userId, int projectId);
        public Task<Dataset> GetDatasetById(int datasetId);
        public Task<LandmarkDto> GetLandmarkById(int landmarkId);
        public Task CreateLandmark(NewLandmark newLandmark);
        public Task<IEnumerable<Observation>> GetObservationsForLandmark(int landmarkId);
        public Task<IEnumerable<User>> GetMembersForProject(int id);
    }
}
