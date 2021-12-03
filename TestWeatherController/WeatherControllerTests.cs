using Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Abstractions;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestWeatherController
{
   public class WeatherControllerTests
    {
        private readonly Mock<IWeatherServices> repoStub = new Mock<IWeatherServices>();


        [Fact]
        public async Task DeleteWeatherRecord_Return_NotFound()
        {
            //arrange
            repoStub.Setup(repo => repo.DeleteWeatherAsync(It.IsAny<Guid>()));

            var controler = new WeatherController(repoStub.Object);

            var result = await controler.DeleteWeatherRecord(Guid.NewGuid());


            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
