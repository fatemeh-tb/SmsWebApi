using Context;
using DataDomain;
using DataDomain.External;
using DataService;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystemWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScoreController : ControllerBase
	{
		private readonly ScoreDataService _scoreDataService;
		private readonly SmsDbContext _smsContext;


		public ScoreController()
		{
			_scoreDataService = new ScoreDataService();
			_smsContext = new SmsDbContext();
		}

		[HttpPost("/Score")]
		public int AddScore(ScoreDto scoreDto)
		{
			return _scoreDataService.AddScores(scoreDto);
		}

		[HttpGet("/scores")]
		public IEnumerable<Scores> GetScores()
		{
			return _scoreDataService.GetScores();
		}
	}
}