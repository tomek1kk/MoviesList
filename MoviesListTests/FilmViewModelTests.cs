using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesList.Models;
using System;
using System.Runtime.CompilerServices;

namespace MoviesListTests
{
    [TestClass]
    public class FilmViewModelTests
    {
        [TestMethod]
        public void GetId_ValidUrl_ShouldReturnValue()
        {
            // Assign
            const string url = "https://swapi.dev/api/films/2/";
            const int expectedId = 2;
            FilmViewModel film = new FilmViewModel()
            {
                Url = url
            };

            // Act
            var id = film.Id;

            // Assert
            Assert.AreEqual(id, expectedId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetId_NoUrl_ShouldThrowException()
        {
            // Assign
            FilmViewModel film = new FilmViewModel();

            // Act
            var id = film.Id;

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetId_InvalidUrl_ShouldThrowException()
        {
            // Assign
            FilmViewModel film = new FilmViewModel()
            {
                Url = "inva/li/d/url"
            };

            // Act
            var id = film.Id;

            // Assert
        }
    }
}
