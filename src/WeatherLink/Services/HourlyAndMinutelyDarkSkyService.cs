// Copyright (c) Adam Weiss. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WeatherLink.Services
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using DarkSky.Models;
	using DarkSky.Services;
	using Microsoft.Extensions.Options;
	using WeatherLink.Models;

	/// <summary>
	/// A service to get a Dark Sky forecast for a latitude and longitude.
	/// </summary>
	public class HourlyAndMinutelyDarkSkyService : IDarkSkyService
	{
		private readonly DarkSkyService.OptionalParameters darkSkyParameters = new DarkSkyService.OptionalParameters { DataBlocksToExclude = new List<ExclusionBlock> { ExclusionBlock.Daily, ExclusionBlock.Alerts } };
		private readonly DarkSkyService darkSkyService;

		/// <summary>
		/// An implementation of IDarkSkyService that exlcudes daily data, alert data, and flags data.
		/// </summary>
		/// <param name="optionsAccessor">Service to access options from startup.</param>
		public HourlyAndMinutelyDarkSkyService(IOptions<WeatherLinkSettings> optionsAccessor)
		{
			darkSkyService = new DarkSkyService(optionsAccessor?.Value.DarkSkyApiKey);
		}

		/// <summary>
		/// Make a request to get forecast data.
		/// </summary>
		/// <param name="latitude">Latitude to request data for in decimal degrees.</param>
		/// <param name="longitude">Longitude to request data for in decimal degrees.</param>
		/// <returns>A DarkSkyResponse with the API headers and data.</returns>
		public async Task<DarkSkyResponse> GetForecast(double latitude, double longitude)
		{
			return await darkSkyService.GetForecast(latitude, longitude, darkSkyParameters);
		}
	}
}