using Microsoft.EntityFrameworkCore;
using Moq;

namespace WorldFacts.Tests;

internal static class DbHelpers
{
    public static DbSet<T> CreateDbSetMock<T>(ICollection<T> elements) where T : class
    {
        var elementsAsQueryable = elements.AsQueryable();
        var dbSetMock = new Mock<DbSet<T>>();

        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

        dbSetMock.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(elements.Add);

        return dbSetMock.Object;
    }
}