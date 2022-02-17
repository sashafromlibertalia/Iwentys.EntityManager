using Bogus;
using Iwentys.EntityManager.Domain;

namespace Iwentys.EntityManager.DataSeeding;

public class StudyGroupFaker : Faker<StudyGroup>
{
    private static readonly Faker _faker = new Faker();

    public StudyGroupFaker()
    {
        // TODO: Proper StudyCourse seeding injection
        CustomInstantiator(f => new StudyGroup(
            MakeGroupName(f).Name,
            new StudyCourse(StudentGraduationYear.Y22, 
                new StudyProgram(f.Random.String()))) {Id = GetId() });
    }

    private int GetId()
    {
        return _faker.IndexVariable++ + 1;
    }

    private static GroupName MakeGroupName(Faker faker)
        => new GroupName(faker.Random.Int(0, 10), faker.Random.Int(10, 100));
}