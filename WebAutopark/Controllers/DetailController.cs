﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebAutopark.BusinessLogicLayer.DataTransferObjects;
using WebAutopark.BusinessLogicLayer.Interfaces;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class DetailController : Controller
    {
        private readonly IDtoService<DetailDto> _detailDtoService;
        private readonly IMapper _mapper;

        public DetailController(IDtoService<DetailDto> dtoService, 
                                IMapper mapper)
        {
            _detailDtoService = dtoService;
            _mapper = mapper;
        }


        //GETALL
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var detailsDto = await _detailDtoService.GetAll();
            var detailsView = _mapper.Map<List<DetailViewModel>>(detailsDto);
            return View(detailsView);
        }

        //CREATE
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DetailViewModel detailViewModel)
        {
            if (ModelState.IsValid)
            {
                var detailDto = _mapper.Map<DetailDto>(detailViewModel);
                await _detailDtoService.Create(detailDto);
            }
            return RedirectToAction("Index");
        }

        //UPDATE
        public async Task<IActionResult> Edit(int id)
        {
            var detailDto = await _detailDtoService.GetById(id);
            if (detailDto != null)
            {
                var detailView = _mapper.Map<DetailViewModel>(detailDto);
                return View(detailView);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DetailViewModel detailViewModel)
        {
            if (ModelState.IsValid)
            {
                var detailDto = _mapper.Map<DetailDto>(detailViewModel);
                await _detailDtoService.Update(detailDto);
            }
            return RedirectToAction("Index");
        }

        //DELETE
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var detailDto = await _detailDtoService.GetById(id);
            if (detailDto != null)
            {
                var detailView = _mapper.Map<DetailViewModel>(detailDto);
                return View(detailView);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _detailDtoService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
