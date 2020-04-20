using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZnymkyHub.DAL.Core;

namespace ZnymkyHub.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ActivityController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly IUnitOfWork _unitOfWork;

        private readonly Dictionary<string, string> WeekDays = new Dictionary<string, string>
        {
            {"Monday", "Mon" },
            {"Tuesday", "Tues" },
            {"Wednesday", "Wed" },
            {"Thursday", "Thurs" },
            {"Friday", "Fri" },
            {"Saturday", "Sat" },
            {"Sunday", "Sun" }
        };

        public ActivityController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Activity()
        {
            var userId = _caller.Claims.FirstOrDefault(c => c.Type == "id");
            var id = Convert.ToInt32(userId.Value);

            var dateToCompare = DateTime.Now.Date.AddDays(-6);
            var activity = await _unitOfWork.Context.ProfileActivities
                .Where(a => a.ProfileId == id && a.Date >= dateToCompare)
                .ToListAsync();

            var groupedActivity = (from a in activity
                       group a by a.Date.ToString("dddd") into g
                       select new { day = g.Key, count = g.ToList().Count }).ToDictionary(d => d.day, d => d.count);

            var week = GetPreviousSevenDays();
            var weekData = week.Select(d => new
            {
                Day = d,
                Count = groupedActivity.ContainsKey(d) ? groupedActivity[d] : 0
            }).ToList();

            return new OkObjectResult(new
            {
                option = new
                {
                    series = new [] { new { name = "Visitors", data = weekData.Select(v => v.Count).ToArray() } },
                    xaxis = new { categories = weekData.Select(v => v.Day).ToArray() }
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> LikesSaved()
        {
            var userId = _caller.Claims.FirstOrDefault(c => c.Type == "id");
            var id = Convert.ToInt32(userId.Value);

            var dateToCompare = DateTime.Now.Date.AddDays(-6);
            var likes = await (from l in _unitOfWork.Context.Likes
                                join p in _unitOfWork.Context.Photos
                                on l.PhotoId equals p.Id
                                where l.Date >= dateToCompare && p.PhotographerId == id
                                select l).ToListAsync();

            var savings = await (from l in _unitOfWork.Context.Savings
                               join p in _unitOfWork.Context.Photos
                               on l.PhotoId equals p.Id
                               where l.Date >= dateToCompare && p.PhotographerId == id
                               select l).ToListAsync();

            var groupedLikes = (from l in likes
                                   group l by l.Date.ToString("dddd") into g
                                   select new { day = g.Key, count = g.ToList().Count }).ToDictionary(d => d.day, d => d.count);
            var groupedSavings = (from s in savings
                                group s by s.Date.ToString("dddd") into g
                                select new { day = g.Key, count = g.ToList().Count }).ToDictionary(d => d.day, d => d.count);

            var week = GetPreviousSevenDays();
            var weekData = week.Select(d => new
            {
                Day = d,
                LikesCount = groupedLikes.ContainsKey(d) ? groupedLikes[d] : 0,
                SavingsCount = groupedSavings.ContainsKey(d) ? groupedSavings[d] : 0
            }).ToList();

            return new OkObjectResult(new
            {
                option = new
                {
                    series = new[] {
                        new { name = "Likes", data = weekData.Select(v => v.LikesCount).ToArray() },
                        new { name = "Saved", data = weekData.Select(v => v.SavingsCount).ToArray() }
                    },
                    xaxis = new { categories = weekData.Select(v => v.Day).ToArray() }
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> Gender()
        {
            var userId = _caller.Claims.FirstOrDefault(c => c.Type == "id");
            var id = Convert.ToInt32(userId.Value);

            var visitorsIds = await _unitOfWork.Context.ProfileActivities
                .Where(a => a.ProfileId == id && a.VisitorId.HasValue)
                .Select(a => (int)a.VisitorId)
                .ToListAsync();

            var genders = await _unitOfWork.Context.Users
                .Where(u => visitorsIds.Contains(u.Id))
                .Select(u => u.Gender)
                .GroupBy(g => g)
                .Select(g => new {
                    Gender = g.Key,
                    Count = g.Count()
                })
                .ToDictionaryAsync(d => d.Gender, d => d.Count);

            var visitorsData = new[] { "Male", "Female" }
                .Select(v => new
                {
                    Gender = v,
                    Count = genders.ContainsKey((DAL.Core.Domain.Gender)Enum.Parse(typeof(DAL.Core.Domain.Gender), v))
                        ? genders[(DAL.Core.Domain.Gender)Enum.Parse(typeof(DAL.Core.Domain.Gender), v)]
                        : 0
                }).ToList();

            if(visitorsData.Sum(v => v.Count) == 0)
            {
                return new OkObjectResult(new
                {
                    option = new
                    {
                        series = new string[] { },
                        labels = new string[] { }
                    }
                });
            }

            return new OkObjectResult(new
            {
                option = new
                {
                    series = visitorsData.Select(v => v.Count).ToArray(),
                    labels = visitorsData.Select(v => v.Gender).ToArray()
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> Age()
        {
            var userId = _caller.Claims.FirstOrDefault(c => c.Type == "id");
            var id = Convert.ToInt32(userId.Value);

            var visitorsIds = await _unitOfWork.Context.ProfileActivities
                .Where(a => a.ProfileId == id && a.VisitorId.HasValue)
                .Select(a => (int)a.VisitorId)
                .ToListAsync();

            var visitorsCount = visitorsIds.Distinct().Count();

            var ageData = await (from u in _unitOfWork.Context.Users
                                    where visitorsIds.Contains(u.Id)
                                    select new { Id = u.Id, Birthday = u.Birthday }).ToListAsync();

            var dict = new Dictionary<string, int>
            {
                { "+12", 0 },
                { "13-17", 0 },
                { "18-24", 0 },
                { "25-34", 0 },
                { "35-44", 0 },
                { "45-54", 0 },
                { "55-64", 0 },
                { "65+", 0 }
            };

            ageData.ForEach(a =>
            {
                var age = a.Birthday.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - a.Birthday.Year))
                                     ? DateTime.Today.Year - a.Birthday.Year - 1
                                     : DateTime.Today.Year - a.Birthday.Year;
                var range = age <= 12
                                ? "+12"
                                : age >= 13 && age <= 17
                                ? "13-17"
                                : age >= 18 && age <= 24
                                ? "18-24"
                                : age >= 25 && age <= 34
                                ? "25-34"
                                : age >= 35 && age <= 44
                                ? "35-44"
                                : age >= 45 && age <= 54
                                ? "45-54"
                                : age >= 55 && age <= 64
                                ? "55-64"
                                : "65+";
                dict[range]++;
            });

            var visitorsData = dict
                .Select(v => new
                {
                    Range = v.Key,
                    Percentage = v.Value * 100 / (visitorsCount == 0 ? 1 : visitorsCount)
                }).ToList();

            return new OkObjectResult(new
            {
                option = new
                {
                    series = new[] { new { name = "Percentage", data = visitorsData.Select(a => a.Percentage).ToArray() } },
                    xaxis = new { categories = visitorsData.Select(v => v.Range).ToArray() }
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> Location()
        {
            var userId = _caller.Claims.FirstOrDefault(c => c.Type == "id");
            var id = Convert.ToInt32(userId.Value);

            var visitorsIds = await _unitOfWork.Context.ProfileActivities
                .Where(a => a.ProfileId == id && a.VisitorId.HasValue)
                .Select(a => (int)a.VisitorId)
                .ToListAsync();

            var visitorsCount = visitorsIds.Distinct().Count();

            var cities = await _unitOfWork.Context.Users
                .Where(u => visitorsIds.Contains(u.Id))
                .Select(u => new { u.HomeTown })
                .ToListAsync();

            var groupedActivity = (from a in cities
                                   group a by a.HomeTown into g
                                   select new { city = g.Key, count = g.Count() } into selection
                                   orderby selection.count descending
                                   select selection
                                   ).ToDictionary(d => d.city != null ? d.city : "Others", d => d.count);

            var locationData = groupedActivity.Select(l => new
            {
                City = l.Key,
                Percentage = l.Value * 100 / visitorsCount
            }).ToList();

            var locationData2 = locationData.Where(l => l.City != "Others").Take(7).ToList();
            var locationData3 = locationData.Except(locationData2);
            int sum = locationData3.Sum(l => l.Percentage);
            if (sum > 0)
            {
                locationData2.Add(new { City = "Others", Percentage = sum });
            }

            return new OkObjectResult(new
            {
                option = new
                {
                    series = new[] { new { name = "Percentage", data = locationData2.Select(a => a.Percentage).ToArray() } },
                    xaxis = new { categories = locationData2.Select(v => v.City).ToArray() }
                }
            });
        }

        private List<string> GetPreviousSevenDays()
        {
            var today = DateTime.Now;
            var list = new List<DateTime>
            {
                today, today.AddDays(-1), today.AddDays(-2), today.AddDays(-3), today.AddDays(-4), today.AddDays(-5), today.AddDays(-6)
            };
            list.Reverse();

            return list.Select(d => d.ToString("dddd")).ToList();
        }
    }
}
