using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab6.Models;

namespace Lab6.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly Lab6Context _context;

        //public Lab6Context db = new Lab6Context();

        public ReviewsController(Lab6Context context)
        {
            _context = context;
        }

        // GET: Reviews
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Reviews.ToListAsync());
        //}

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["MovieSortParm"] = sortOrder == "Movie" ? "movie_desc" : "Movie";
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["MovieSortParm"] = String.IsNullOrEmpty(sortOrder) ? "movie_desc" : "";
            var reviews = from s in _context.Reviews
                           select s;

            var movies = from m in _context.Movie
                         select m;

            foreach (var item in movies)
            {
                foreach (var r in reviews)
                {
                    if (item.ID == r.MovieID)
                    {
                        ViewData["mTitle"] = item.Title;
                        ViewData["mID"] = r.MovieID;
                    }
                }
            }



            switch (sortOrder)
            {
                case "Name":
                    reviews = reviews.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    reviews = reviews.OrderByDescending(s => s.Name);
                    break;
                case "Movie":
                    reviews = reviews.OrderBy(s => s.MovieID);
                    break;
                case "movie_desc":
                    reviews = reviews.OrderByDescending(s => s.MovieID);
                    break;
                //default:
                //    reviews = reviews.OrderBy(s => s.Name);
                //    break;
            }

            var movieGenreVM = new MovieGenreViewModel();
            movieGenreVM.genres = new SelectList(await reviews.AsNoTracking().ToListAsync());
            movieGenreVM.movies = await movies.ToListAsync();

            var movieReviewVM = new ReviewList();
            //movieReviewVM.movie = movie;
            movieReviewVM.mReview = new SelectList(await reviews.AsNoTracking().ToListAsync());
            movieReviewVM.review = await reviews.ToListAsync();

            var movieReviewModelVM = new MovieReview();
            movieReviewModelVM.MGenre = movieGenreVM;
            movieReviewModelVM.MReview = movieReviewVM;

            return View(movieReviewModelVM);

            //return View(await reviews.AsNoTracking().ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .SingleOrDefaultAsync(m => m.ID == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // GET: Reviews/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // GET: Reviews/Create
        public IActionResult Create(int? id)
        {
            var movies = from m in _context.Movie
                         select m;

            foreach (var item in movies)
            {
                if (item.ID == id)
                {
                    ViewData["mTitle"] = item.Title;
                    ViewData["mID"] = id;
                }
            }
            return View();
        }


        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,MovieReview,MovieID")] Reviews reviews, int? id)
        {
            if (ModelState.IsValid)
            {
                reviews.MovieID = (int)id;
                _context.Add(reviews);

                ViewData["mID"] = reviews.MovieID;
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Movies", new { id = reviews.MovieID });
            }
            return View(reviews);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews.SingleOrDefaultAsync(m => m.ID == id);
            if (reviews == null)
            {
                return NotFound();
            }

            ViewData["mID"] = reviews.MovieID;

            var movies = from m in _context.Movie
                         select m;

            foreach (var item in movies)
            {
                if (item.ID == reviews.MovieID)
                {
                    ViewData["mTitle"] = item.Title;
                }
            }

            return View(reviews);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,MovieReview,MovieID")] Reviews reviews)
        {
            if (id != reviews.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewsExists(reviews.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Movies", new { id = reviews.MovieID });
            }
            return View(reviews);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .SingleOrDefaultAsync(m => m.ID == id);
            if (reviews == null)
            {
                return NotFound();
            }
            else
            {
                var movies = from m in _context.Movie
                             select m;

                foreach (var item in movies)
                {
                    if (item.ID == reviews.MovieID)
                    {
                        ViewData["mTitle"] = item.Title;
                        ViewData["mID"] = item.ID;
                    }
                }
            }

            return View(reviews);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviews = await _context.Reviews.SingleOrDefaultAsync(m => m.ID == id);
            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Movies", new { id = reviews.MovieID });
        }

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.ID == id);
        }
    }
}
