using FuglariApi.Models;
using FuglariApi.RequestModels;
using FuglariApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        public ProjectController(IProjectService service)
        {
            projectService = service;
        }

        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetProjectsForPerson(string email)
        {
            return Ok(await projectService.GetProjectsForUser(email));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewProject([FromBody] CreateProjectRequest request)
        {
            await projectService.CreateProject(request);
            return Ok();
        }
        [HttpPost("dataset")]
        public async Task<IActionResult> CreateDataset([FromBody] CreateDatasetRequest createDatasetRequest)
        {
            await projectService.CreateDataset(createDatasetRequest);
            return Ok();
        }
        [HttpGet("{projectId}/datasets")]
        public async Task<IActionResult> GetDatasetsForProject(int projectId)
        {
            return Ok(await projectService.GetDatasetsForProject(projectId));
        }

        [HttpGet("/datasets/{email}")]
        public async Task<IActionResult> GetDatasetsForPerson(string email)
        {
            return Ok(await projectService.GetDatasetsForPerson(email));
        }
       

        [HttpPost("landmark")]
        public async Task<IActionResult> AddLandmark([FromBody] CreateLandmarkRequest createLandmarkRequest)
        {
            await projectService.CreateLandmark(createLandmarkRequest);
            return Ok();
        }
        [HttpGet("datasets/{datasetId}/landmarks")]
        public async Task<IActionResult> GetLandmarks(int datasetId)
        {
            return Ok(await projectService.GetLandmarksForDataset(datasetId));
        }

        [HttpGet("landmarks/{landmarkId}/observations")]
        public async Task<IActionResult> GetObservationsForLandmark(int landmarkId)
        {
            return Ok(await projectService.GetObservationsForLandmark(landmarkId));
        }

        [HttpPost("landmarks/observations")]
        public async Task<IActionResult> AddObservationsToLandmark([FromBody] PostObservationRequest request)
        {
            await projectService.AddObsevation(request);
            return Ok();
        }
        [HttpPatch("landmarks")]
        public async Task<IActionResult> UpdateLandmark([FromBody] Landmark landmark)
        {
            await projectService.UpdateLandmark(landmark);
            return Ok();
        }
    }
}
