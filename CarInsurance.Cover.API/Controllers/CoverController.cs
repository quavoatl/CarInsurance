using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsurance.DataAccessV2.Repositories.Cover;

namespace CarInsurance.Cover.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoverController : Controller
    {
        private readonly ICoverRepository _coverRepository = null;

        public CoverController(ICoverRepository coverRepository)
        {
            _coverRepository = coverRepository;
        }

        [HttpGet]
        public List<DataAccessV2.DbModels.Cover> Get()
        {
            return _coverRepository.GetCovers();
        }

        [HttpGet("{guid}")]
        public DataAccessV2.DbModels.Cover Get(string guid)
        {
           return _coverRepository.GetCover(guid);
        }

        [HttpPost]
        public void Post([FromBody]DataAccessV2.DbModels.Cover cover)
        {
            if (ModelState.IsValid)
            {
                _coverRepository.Insert(cover);
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, DataAccessV2.DbModels.Cover cover)
        {

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
