using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model;
using LiveHAPI.Model;
using LiveHAPI.Shared.ValueObject;
using LiveHAPI.Shared.ValueObject.Meta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Remotion.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{
    
    [Route("api/meta")]
    public class MetaController : Controller
    {
        private readonly IMetaService _metaService;
        private readonly ILogger<CountiesController> _logger;

        public MetaController(ILogger<CountiesController> logger, IMetaService metaService)
        {
            _logger = logger;
            _metaService = metaService;
        }

        [Route("data")]
        [HttpGet]
        public IActionResult GetData()
        {
            try
            {
                var m=new MetaInfo();
                
                var practiceTypes = _metaService.ReadLookupCategoriesItems().ToList();

                m.Actions = Mapper.Map<List<ActionInfo>>(_metaService.ReadActions().ToList());
                m.IdentifierTypes = Mapper.Map<List<IdentifierTypeInfo>>(_metaService.ReadLookupCategoriesItems().ToList());
                m.RelationshipTypes = Mapper.Map<List<RelationshipTypeInfo>>(_metaService.ReadLookupCategoriesItems().ToList());
                m.KeyPops = Mapper.Map<List<KeyPopInfo>>(_metaService.ReadLookupCategoriesItems().ToList());
                m.MaritalStatuses = Mapper.Map<List<MaritalStatusInfo>>(_metaService.ReadLookupCategoriesItems().ToList());
                m.PracticeTypes = Mapper.Map<List<PracticeTypeInfo>>(_metaService.ReadLookupCategoriesItems().ToList());
                m.Conditions = Mapper.Map<List<ConditionInfo>>(_metaService.ReadLookupCategoriesItems().ToList());
                m.Conditions = Mapper.Map<List<ConceptTypeInfo>>(_metaService.ReadLookupCategoriesItems().ToList());
                m.Validators = Mapper.Map<List<ValidatorTypeInfo>>(_metaService.ReadLookupCategoriesItems().ToList());
                m.ValidatorTypes = Mapper.Map<List<ValidatorInfo>>(_metaService.ReadLookupCategoriesItems().ToList());
                m.EncounterTypes = Mapper.Map<List<EncounterTypeInfo>>(= _metaService.ReadLookupCategoriesItems().ToList());


                var counties = Mapper.Map<List<CategoryItemInfo>>(c);
                return Ok(counties);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"Error loading counties: {e}");
                return StatusCode(500, "Error loading counties");
            }
        }


        [Route("counties")]
        [HttpGet]
        public IActionResult GetCounties()
        {
            try
            {
                var c = _metaService.ReadCounties().ToList();

                var counties = Mapper.Map<List<CountyInfo>>(c);
                return Ok(counties);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Error loading counties");
            }
        }
        [Route("categories")]
        [HttpGet]
        public IActionResult GetLookupCategories()
        {
            try
            {
                var c = _metaService.ReadLookupCategories().ToList();

                var counties = Mapper.Map<List<CategoryInfo>>(c);
                return Ok(counties);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Error loading Categories");
            }
        }
        [Route("items")]
        [HttpGet]
        public IActionResult GetLookupItems()
        {
            try
            {
                var c = _metaService.ReadLookupItems().ToList();

                var counties = Mapper.Map<List<ItemInfo>>(c);
                return Ok(counties);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Error loading items");
            }
        }
        [Route("catitems")]
        [HttpGet]
        public IActionResult GetLookupCatItems()
        {
            try
            {
                var c = _metaService.ReadLookupCategoriesItems().ToList();

                var counties = Mapper.Map<List<CategoryItemInfo>>(c);
                return Ok(counties);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"Error loading counties: {e}");
                return StatusCode(500, "Error loading counties");
            }
        }
    }
}
