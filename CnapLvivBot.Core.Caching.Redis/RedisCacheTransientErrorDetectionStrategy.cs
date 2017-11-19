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
			if (ex == null)
				return false;
			if (ex as TimeoutException != null)
				return true;
			if (ex as RedisServerException != null)
				return true;
			if (ex as RedisException != null)
				return true;

			return ex.InnerException != null && IsTransient(ex.InnerException);
		}
	}
}