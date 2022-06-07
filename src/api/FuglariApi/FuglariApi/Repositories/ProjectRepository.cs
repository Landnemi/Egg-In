using Dapper;
using FuglariApi.Infrastructure;
using FuglariApi.Models;
using FuglariApi.RequestModels;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuglariApi.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private string connectionString;
        public ProjectRepository(IOptions<PsqlSettings> psqlSettings)
        {
            connectionString = "User ID=postgres;Host=178.128.33.244;Port=5432;Database=Fuglari;password=b1rdm4n;Pooling=true;SSL Mode=Disable;TrustServerCertificate=True"; // psqlSettings.Value.ConnectionString;
            //connectionString = "User ID=postgres;Host=localhost;Port=5432;Database=Fuglari;password=b1rdm4n;Pooling=true;Connection Lifetime=0;Encrypt=False;TrustServerCertificate=True"; // psqlSettings.Value.ConnectionString;

        }




        private string insertDatasetSQL = $@"INSERT INTO public.datasets (title, project_id) values (@Title, @ProjectId) returning id
        ";
        public async Task CreateDataset(NewDataset request)
        {
            StringBuilder queryBuilder = new StringBuilder(insertDatasetSQL);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(queryBuilder.ToString(), new { Title=request.Title, ProjectId = request.ProjectId });
                return;// int newId await connection.QueryAsync<int>(queryBuilder.ToString());
            }
        }

        private string insertProjectSQL = $@"INSERT INTO public.projects (title, owner_id) values (@Title, @OwnerId) returning id
        ";
        private string insertMembershipRecordSQL = $@"INSERT INTO public.memberships (user_id, project_id, admin) values (@UserId, @ProjectId, true)";
        public async Task CreateProject(NewProject project)
        {
            StringBuilder queryBuilder = new StringBuilder(insertProjectSQL);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                int newId = await connection.QuerySingleAsync<int>(queryBuilder.ToString(), new { OwnerId = project.UserId, Title = project.ProjectTitle });
                await connection.ExecuteAsync(insertMembershipRecordSQL, new { UserId = project.UserId, ProjectId = newId } );
                return;
            }
        }
        private string selectDatasetsSQL = $@"SELECT 
id as {nameof(Dataset.Id)},
project_id as {nameof(Dataset.ProjectId)},
title as {nameof(Dataset.Title)}
FROM public.datasets";
        public async Task<IEnumerable<Dataset>> GetDatasetsForProject(int projectId)
        {
            StringBuilder queryBuilder = new StringBuilder(selectDatasetsSQL);
            queryBuilder.Append(" where project_id = " + projectId);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<Dataset>(queryBuilder.ToString());
                
            }
        }

        private string selectLandmarksSQL = $@"SELECT 
id as {nameof(Landmark.Id)},
dataset_id as {nameof(Landmark.DatasetId)},
longitude as {nameof(Landmark.Longitude)},
latitude as  {nameof(Landmark.Latitude)},
status as {nameof(Landmark.Status)},
progress as {nameof(Landmark.Progress)}
FROM public.landmarks";
        public async Task<IEnumerable<Landmark>> GetLandmarksForDataset(int datasetId)
        {
            StringBuilder queryBuilder = new StringBuilder(selectLandmarksSQL);
            queryBuilder.Append(" where dataset_id = " + datasetId);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<Landmark>(queryBuilder.ToString());
            }
        }
        private string selectProjectMembershipsSQL = $@"SELECT 
        p.id as {nameof(Project.Id)},
        p.title as {nameof(Project.Title)},
        p.owner_id as {nameof(Project.OwnerId)}
        from public.projects p
        right join (select * from public.memberships m where m.user_id = @UserId ) sq on sq.project_id = p.id
        ";
        public async Task<IEnumerable<Project>> GetProjectsForUser(int userId)
        {
            StringBuilder queryBuilder = new StringBuilder(selectProjectMembershipsSQL);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<Project>(queryBuilder.ToString(), new { UserId =userId });
            }
        }
        private string updateLandmarkSQL = $@"UPDATE public.landmarks l SET  status = @Status, progress = @Progress ";
        public async Task UpdateLandmark(Landmark landmark)
        {
            StringBuilder queryBuilder = new StringBuilder(updateLandmarkSQL);
            queryBuilder.Append($@" where l.id = {landmark.Id}");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(queryBuilder.ToString(), new {Status=landmark.Status, Progress=landmark.Progress });
                return;
            }
        }
        private string addUserToProjectSQL = $@"INSERT INTO public.memberships (user_id, project_id) values (@UserId, @ProjectId)";
        public async Task AddUserToProject(int userId, int projectId)
        {
            StringBuilder queryBuilder = new StringBuilder(addUserToProjectSQL);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(queryBuilder.ToString(), new { UserId=userId,ProjectId = projectId });
                return;
            }
        }
        private string adObservationToLandmarkSQL = $@"INSERT INTO public.observation (user_id, landmark_id, comment) values (@UserId, @LandmarkId, @Comment)";
        public async Task AddObservationToLandmark(NewObservation newObservation)
        {
            StringBuilder queryBuilder = new StringBuilder(adObservationToLandmarkSQL);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(queryBuilder.ToString(), new { UserId = newObservation.UserId, ProjectId = newObservation.LandmarkId, Comment=newObservation.Comment});
                return;
            }
        }
        private string selectMembershipsSQL = $@"SELECT 
id as {nameof(Membership.Id)},
user_id as {nameof(Membership.UserId)},
project_id as {nameof(Membership.ProjectId)},
admin as {nameof(Membership.Admin)}
FROM public.memberships";
        public async Task<Membership> CheckMembership(int userId, int projectId)
        {
            StringBuilder queryBuilder = new StringBuilder(selectMembershipsSQL);
            queryBuilder.Append(" where user_id = " + userId + " and project_id = "+ projectId);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<Membership>(queryBuilder.ToString() );    
            }
        }

        private string selectProjectSQL = $@"SELECT p.id as {nameof(Project.Id)},
        p.title as {nameof(Project.Title)},
        p.owner_id as {nameof(Project.OwnerId)}
        from public.projects p";
        public async Task<Project> GetProjectById(int projectId)
        {
            StringBuilder queryBuilder = new StringBuilder(selectProjectSQL);
            queryBuilder.Append(" where p.id = " + projectId);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<Project>(queryBuilder.ToString());
            }
        }

        public async Task<Dataset> GetDatasetById(int datasetId)
        {
            StringBuilder queryBuilder = new StringBuilder(selectDatasetsSQL);
            queryBuilder.Append(" where id = " + datasetId);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<Dataset>(queryBuilder.ToString());
            }
        }

        public async Task<Landmark> GetLandmarkById(int landmarkId)
        {
            StringBuilder queryBuilder = new StringBuilder(selectLandmarksSQL);
            queryBuilder.Append(" where id = " + landmarkId);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<Landmark>(queryBuilder.ToString());
            }
        }
        public string createLandmarkSQL = $@"INSERT INTO public.landmarks (dataset_id, latitude, longitude, status, progress) VALUES (@DatasetId, @Latitude, @Longitude, @Status, @Progress)";
        public async Task CreateLandmark(NewLandmark newLandmark)
        {
            StringBuilder queryBuilder = new StringBuilder(createLandmarkSQL);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(queryBuilder.ToString(), newLandmark);
            }
        }
        private string selectObservationSQL = $@"SELECT 
        o.id as {nameof(Observation.Id)},
        o.landmark_id as {nameof(Observation.LandmarkId)},
        o.comment as {nameof(Observation.Comment)},
        o.user_id as {nameof(Observation.UserId)},
        o.datetime as {nameof(Observation.DateCreated)}
        from public.observations o";
        public async Task<IEnumerable<Observation>> GetObservationsForLandmark(int landmarkId)
        {
            StringBuilder queryBuilder = new StringBuilder(selectObservationSQL);
            queryBuilder.Append(" where o.landmark_id = " + landmarkId);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<Observation>(queryBuilder.ToString());
            }
        }
    }
}
