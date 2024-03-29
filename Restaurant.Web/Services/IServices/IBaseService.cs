﻿using Restaurant.Web.Models;

namespace Restaurant.Web.Services.IServices;

public interface IBaseService
{
	public ResponseDto responseModel { get; set; }

	public Task<T> SendAsync<T>(ApiRequest apiRequest);
}
