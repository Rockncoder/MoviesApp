using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
//using System.Web.Http;
using System.Web.Mvc;
using MoviesApp.Models;

namespace MoviesApp.Controllers
{
	public class MobileController : Controller
	{
		private MoviesDBEntities1 _db = new MoviesDBEntities1(); 

		//
		// GET: /Home/ 
		public ActionResult Index()
		{
			return View(_db.Movies.ToList());
		}


		//
		// GET: /Home/Details/5 
		public ActionResult Delete(int id)
		{
			var movieToEdit = (from m in _db.Movies
							   where m.Id == id
							   select m).First();
			return View(movieToEdit);
		}

		// POST: /Home/Edit/5 
		[HttpPost, ActionName("Delete")]
		public ActionResult Delete(Movie movieToDelete)
		{
			var originalMovie = (from m in _db.Movies
								 where m.Id == movieToDelete.Id
								 select m).First();

			if (!ModelState.IsValid)
				return View(originalMovie);

			_db.Movies.Remove(originalMovie);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		//
		// GET: /Home/Details/5 
		public ActionResult Details(int id)
		{
			var movieToEdit = (from m in _db.Movies
							   where m.Id == id
							   select m).First();
			return View(movieToEdit);
		}

		//
		// GET: /Home/Create 
		public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /Home/Create 
		[HttpPost]
		public ActionResult Create([Bind(Exclude = "Id")] Movie movieToCreate)
		{
			if (!ModelState.IsValid)
				return View();

			_db.Movies.Add(movieToCreate);
			_db.SaveChanges();
			return RedirectToAction("Index");
		} 

		//
		// GET: /Home/Edit/5
		public ActionResult Edit(int id)
		{
			var movieToEdit = (from m in _db.Movies
								where m.Id == id
								select m).First();
			return View(movieToEdit);
		}

		//
		// POST: /Home/Edit/5 
		[HttpPost]
		public ActionResult Edit(Movie movieToEdit)
		{
			var originalMovie = (from m in _db.Movies 
									where m.Id == movieToEdit.Id 
									select m).First(); 

			if (!ModelState.IsValid)
				return View(originalMovie);

			_db.Entry(originalMovie).CurrentValues.SetValues(movieToEdit);
			_db.SaveChanges(); 
			return RedirectToAction("Index");
		}
	}
}
