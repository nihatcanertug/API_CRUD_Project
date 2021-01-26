using API_CRUD_Project.DataAccess.Repositories;
using API_CRUD_Project.Models;
using API_CRUD_Project.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD_Project.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BoxerController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public BoxerController(IRepository repository,
                               IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBoxers()
        {
            var objectList = _repository.GetBoxer();     // We pulled the boxers from the database and assigned them to the "objectList" variable.

            var objeDto = new List<BoxerDto>();

            foreach (var obj in objectList)
            {
                objeDto.Add(_mapper.Map<BoxerDto>(obj));
                // We automatically matched the Boxer information we filled into the "objectList" object with the properties of the BoxerDto object that we prepared on a business basis, and the fields we created over the job, that is, the fields we needed, were sent to us.
            }

            return Ok(objeDto);
            // The Ok () method here is to return 200 status codes whose body is embedded in the Http protocol, and we have sent the object that we mapped.
        }

        // When the application was run, we saw that "GetBoxers" methods collided. To get rid of this, we'll value the "[HttpGet]" attribute

        [HttpGet("{id}", Name = "GetBoxers")]
        public IActionResult GetBoxers(int id)
        {
            Boxer boxer = _repository.GetBoxer(id);

            if (boxer == null) return NotFound();

            var boxerDto = _mapper.Map<BoxerDto>(boxer);

            return Ok(boxerDto);
        }

        // Note: "Content-Type: application / json" information is added to the header part of the requests sent in requests such as Post, Put, Patch, Delete. Otherwise, media type error is experienced.
        [HttpPost]
        public IActionResult CreateBoxer([FromBody] BoxerDto data)
        {
            // We can post the data of the entity to be created in 2 ways. The first of these is "[FromBody]", which we will prefer when we send the data from the body of the request. In the second "[FromUri]" approach, it is necessary to send a value to the propery of the asset we want to post from the browser address bar. It is unnecessary as it is a laborious task and we will use any of the tools like (Postman-Fiddler-Swashbuckle) as the interface in this project while testing the api.

            if (ModelState.IsValid)
            {
                if (data == null) return BadRequest(data); // Return Status Code 400 

                if (_repository.IsBoxerExsist(data.FullName))// We checked if there is any boxer with the same name in the database. If there is, it will return True and the if block will be activated.
                {
                    ModelState.AddModelError("", "The boxer already exsist..!");
                    return StatusCode(404, ModelState);
                }

                var boxerObject = _mapper.Map<Boxer>(data);
                // Since we used assets in the repository in previous projects, we could not use Data Transfer Objects in our methods in the repository. We have solved this big problem with AutoMapper.

                if (!_repository.Create(boxerObject)) // In this project, we have defined our methods to carry out CRUD operations as "boolean", mainly of return types. Here, if the insertion fails, we go through the scenario.

                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {data.FullName}");
                    return StatusCode(500, ModelState);
                }

                // return CreatedAtRoute ("GetBoxers", boxerObject); // GetBoxers action git, go with the data of the boxer created while going
                // In the following redirect, it takes the id parameter from the two "GetBoxers" methods and only requests the route to be the method that brings the boxer according to the parameter it gets. It will also carry the data of the boxer created like the method above while going.

                return CreatedAtRoute("GetBoxers", new { id = boxerObject.Id }, boxerObject);
            }

            return BadRequest();
        }
        // HttpPut vs HttpPatch
        // If we use "HttpPut" in our update operation, the object itself is sent, not as a collection. In other words, when "Put" is used, all fields of the entity are sent or included in the process. "Patch" sends only the areas to be changed.

        // Using "Put" or "Pach" in the process below will not change the result of the operation. Both will perform the update process. Patch will only do this by updating the fields provided while Put is changing the entire asset. Buddha returns to us as a performance. Because changing all fields of an asset at the same time is not a very possible scenario.

        [HttpPatch("{id}", Name = "UpdateBoxer")]
        public IActionResult UpdateBoxer(int id, [FromBody] BoxerDto data)
        {
            if (data == null) return BadRequest(data);

            var boxerObject = _mapper.Map<Boxer>(data);

            if (!_repository.Update(boxerObject))
            {
                ModelState.AddModelError("", $"Something went wrong when editing record {data.FullName}");
                return StatusCode(500, data);
            }

            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteBoxer")]
        public IActionResult DeleteBoxer(int id)
        {
            Boxer boxer = _repository.GetBoxer(id);

            if (!_repository.IsBoxerExsist(boxer.Id)) return NotFound();

            if (!_repository.Delete(boxer))
            {
                ModelState.AddModelError("", $"Something went wrong delete record {boxer.FullName}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
