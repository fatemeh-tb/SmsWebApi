using Microsoft.EntityFrameworkCore;

namespace Context;

public class SqliteSmsDbContext: SmsDbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		options.UseSqlite(@$"DataSource=sms.db;");
	}
}