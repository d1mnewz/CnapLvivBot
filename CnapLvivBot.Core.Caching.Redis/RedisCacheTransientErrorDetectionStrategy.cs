using System;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using StackExchange.Redis;

namespace CnapLvivBot.Core.Caching.Redis
{
	public class RedisCacheTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
	{
		/// <summary>
		///     Custom Redis Transient Error Detection Strategy must have been implemented to satisfy Redis exceptions.
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public bool IsTransient(Exception ex)
		{
			switch (ex)
			{
				case null:
					return false;
				case TimeoutException _:
					return true;
				case RedisServerException _:
					return true;
				case RedisException _:
					return true;
			}

			return ex.InnerException != null && IsTransient(ex.InnerException);
		}
	}
}