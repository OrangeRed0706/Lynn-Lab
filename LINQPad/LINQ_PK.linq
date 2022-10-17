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
	var summary = BenchmarkRunner.Run<ForPkLinQ>();
}

public class ForPkLinQ
{
	private readonly List<User> users = new List<User>();
	private readonly string value = "test";
	public ForPkLinQ()
	{
		PrepareTestUsers();
	}

	private void PrepareTestUsers()
	{
		for (var i = 0; i < 10000000; i++)
		{
			users.Add(new User { Id = 0, Name = i % 3 == 0 ? $"test {i}" : $"hello {i}" });
		}
	}

	[Benchmark]
	public bool GetByLinqWhereAny()
	{
		return users.Where(x => x.Name.Contains(value)).Any();
	}

	[Benchmark]
	public bool GetByLinqAny()
	{
		return users.Any(x => x.Name.Contains(value));
	}
}
public class User
{
	public int Id { get; set; }
	public string Name { get; set; }
}