using Context;
using DataDomain;
using DataDomain.External;

namespace DataService;

public class ScoreDataService
{
	private readonly SmsDbContext _smsContext;

	public ScoreDataService()
	{
		_smsContext = new SmsDbContext();
		_smsContext = new SqliteSmsDbContext();
		_smsContext.Database.EnsureCreated();
	}


	public int AddScores(ScoreDto scoresDto)
	{
		var score = new Scores();

		var dbScore = _smsContext.Scores.SingleOrDefault(i => i.StudentId == scoresDto.StudentId);

		if (dbScore is null)
		{
			score.Score1 = scoresDto.Score1;
			score.Score2 = scoresDto.Score2;
			score.Score3 = scoresDto.Score3;
			score.Score4 = scoresDto.Score4;
			score.FinalScore = scoresDto.FinalScore;
			score.StudentId = scoresDto.StudentId;
			score.CourseId = scoresDto.CourseId;
			_smsContext.Scores.Add(score);
		}
		else
		{
			var scoreToUpdate = _smsContext.Scores.FirstOrDefault(s => s.Id == scoresDto.Id);
			if (scoreToUpdate is not null)
			{
				scoreToUpdate.Score1 = scoresDto.Score1;
				scoreToUpdate.Score2 = scoresDto.Score2;
				scoreToUpdate.Score3 = scoresDto.Score3;
				scoreToUpdate.Score4 = scoresDto.Score4;
				scoreToUpdate.FinalScore = scoresDto.FinalScore;
			}
		}

		return _smsContext.SaveChanges();
	}

	public IEnumerable<Scores> GetScores()
	{
		return _smsContext
			.Scores
			.Select(s => new Scores()
			{
				Id = s.Id,
				Score1 = s.Score1,
				Score2 = s.Score2,
				Score3 = s.Score3,
				Score4 = s.Score4,
				FinalScore = s.FinalScore,
				Student = s.Student,
				Course = s.Course,
				StudentId = s.StudentId,
				CourseId = s.CourseId
			}).ToList();
	}
}