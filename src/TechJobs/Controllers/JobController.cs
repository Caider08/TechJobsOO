﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            List<Job> jobz = new List<Job>();

            // TODO #1 - get the Job with the given ID and pass it into the view
            SearchJobsViewModel searchJobsViewModel = new SearchJobsViewModel();
            jobz.Add(jobData.Find(id));
            searchJobsViewModel.Jobs = jobz;

            return View(searchJobsViewModel);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        [Route("Job/New")]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            if (ModelState.IsValid)
            {
                Job newJob = new Models.Job
                {
                    Name = newJobViewModel.Name,
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = jobData.Locations.Find(newJobViewModel.Location),
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetency),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionType),
                };
                



                jobData.Jobs.Add(newJob);
                //return Redirect("/job?id=" + newJob.ID);
                int numba = newJob.ID; ;
                return RedirectToAction("Index", "Job", new { id = numba });
                //return Index(numba);
            }

            else
            {
                return View(newJobViewModel);
            }

            

        }
    }     
}

 

 