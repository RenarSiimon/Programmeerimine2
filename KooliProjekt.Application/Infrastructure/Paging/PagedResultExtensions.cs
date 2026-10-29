using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Paging
{
	public static class PagedResultExtensions
	{
		public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
		{
			var count = await query.CountAsync(cancellationToken);
			var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

			return new PagedResult<T>
			{
				PageIndex = pageIndex,
				PageSize = pageSize,
				TotalCount = count,
				TotalPages = (int)Math.Ceiling(count / (double)pageSize),
				Results = items
			};
		}
	}
}