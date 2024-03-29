﻿using Condominios.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Condominios.Controllers
{
    public class HomeController : Controller
    {
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        [Authorize(Roles = "Administrador, General")]
        public IActionResult Pendientes()
        {
            return RedirectToAction("Pendientes", "Mantenimientos");
        }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        [Authorize(Roles = "Administrador, General")]
        public IActionResult Catalogos()
        {
            return RedirectToAction("Index", "Catalogos");
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        //[Authorize(Roles = "Administrador, General")]
        public IActionResult Proveedores() 
        {
            return RedirectToAction("Index", "Proveedores");
        }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        [Authorize(Roles = "Administrador, General")]
        public IActionResult TipoEquipos()
        {
            return RedirectToAction("Index", "TipoEquipos");
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        [Authorize(Roles = "Administrador, General")]
        public IActionResult Equipos()
        {
            return RedirectToAction("Index", "Equipos");
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        [Authorize(Roles = "Administrador, General")]
        public IActionResult GastosDeMantenimiento()
        {
            return RedirectToAction("GastosMantenimiento", "Mantenimientos");
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        [Authorize(Roles = "Administrador, General")]
        public IActionResult Mantenimientos()
        {
            return RedirectToAction("Crear", "Mantenimientos");
        }


        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        [Authorize(Roles = "Administrador, General")]
        public IActionResult Privacy()
        {
            return View();
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}