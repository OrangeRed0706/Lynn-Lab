<Query Kind="Program">
  <Connection>
    <ID>4f0fde79-5c18-4d40-857a-a0190bd58737</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="EF7Driver" PublicKeyToken="469b5aa5a4331a8c">EF7Driver.StaticDriver</Driver>
    <CustomAssemblyPath>C:\Higgsworkspace\products\Higgs.Data.NumberGame.Context\bin\Debug\net6.0\Higgs.Data.NumberGame.Context.dll</CustomAssemblyPath>
    <CustomTypeName>Higgs.Data.NumberGame.Context.NumberGameContext</CustomTypeName>
    <CustomCxString>Server=172.16.5.7;Port=5432;Database=NumberGame;User Id=postgres;Password=Sa12345678;Timeout=30;Application Name=Product NumberGame Api;Minimum Pool Size=8;Maximum Pool Size=1024;</CustomCxString>
    <DriverData>
      <UseDbContextOptions>true</UseDbContextOptions>
      <EFProvider>Npgsql.EntityFrameworkCore.PostgreSQL</EFProvider>
    </DriverData>
  </Connection>
  <Reference>C:\Higgsworkspace\products\Higgs.Data.NumberGame.Entities\bin\Debug\netstandard2.0\Higgs.Data.NumberGame.Entities.dll</Reference>
  <Reference>C:\Higgsworkspace\products\Higgs.Data.NumberGame.Entities\bin\Debug\netstandard2.0\Higgs.Data.NumberGame.Values.dll</Reference>
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>BenchmarkDotNet.Running</Namespace>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
  <Namespace>Higgs.Data.NumberGame.Entities</Namespace>
</Query>

void Main()
{
	Wagers.ToList().Where(x=> x.BetTypeId > 0).Any().Dump();
	Wagers.Any(x=> x.BetTypeId > 0).Dump();
	Wagers.Count(x=> x.BetTypeId > 0).Dump();
	//var context = this.Wagers;
		//IEnumerable<Wager> wager = this.Wagers;
	//var test = new PkLinQ(50000,wager);
	
}
public class PkLinQ
{
	public int _count;
	//public DbSet<Wager> _wager;
	public IEnumerable<Wager> _wager;
	public PkLinQ(int count,IEnumerable<Wager> wager)
	{
		_count = count;
		_wager = wager;
		Run();
	}
	void Run()
	{
		Stopwatch sw1 = new Stopwatch();
		Stopwatch sw2 = new Stopwatch();
		sw1.Start();
		for (var i = 0; i < _count; i++)
		{
			GetByLinqAny();
		}
		sw1.Stop();
		Console.WriteLine($"GetByLinqAny spend {sw1.ElapsedMilliseconds} ms" );
		sw2.Start();
		for (var i = 0; i < _count; i++)
		{
			GetByLinqWhereAny();
		}
		sw2.Stop();
		Console.WriteLine($"GetByLinqWhereAny spend {sw2.ElapsedMilliseconds} ms" );
	}

	public bool GetByLinqWhereAny()
	{
		return _wager.Where(x => x.BetTypeId > 0).Any();
	}

	public bool GetByLinqAny()
	{
		return _wager.Any(x => x.BetTypeId > 0);
	}
}
// You can define other methods, fields, classes and namespaces here