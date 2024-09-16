using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hydrogen.Repo.Abstractions.DTO;
using SqlKata;

namespace Hydrogen.Repo.Abstractions
{
    public interface IHydrogenRepository<TModel, TId> where TModel : IHydrogenModel<TId>
    {
        /// <summary>
        /// Checks that at least one record complies with the specified condition.
        /// </summary>
        /// <param name="column">
        /// Column to be compared
        /// </param>
        /// <param name="value">
        /// Value to be compared
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// True if at least one record in the specified column is equal to the comparison value, otherwise false.
        /// </returns>
        Task<bool> AnyAsync(string column, object value, CancellationToken ct = default);

        /// <summary>
        /// Checks that at least one record complies with the specified condition.
        /// </summary>
        /// <param name="column">
        /// Column to be compared
        /// </param>
        /// <param name="op">
        /// Comparison operator
        /// </param>
        /// <param name="value">
        /// Value to be compared
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        ///True if at least one record in the specified column is equal to the comparison value, otherwise false.
        /// </returns>
        Task<bool> AnyAsync(string column, string op, object value, CancellationToken ct = default);

        /// <summary>
        /// Checks if any record with the specified id exists
        /// </summary>
        /// <param name="id">
        /// Identifier to be checked
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// True if at least one record in the specified column is equal to the comparison value, otherwise false.
        /// </returns>
        Task<bool> AnyAsync(TId id, CancellationToken ct = default);

        /// <summary>
        /// Checks if there is at least one record in the table
        /// </summary>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// True if any record exists, false otherwise
        /// </returns>
        Task<bool> AnyAsync(CancellationToken ct = default);

        /// <summary>
        /// Checks if at least one record meets the conditions specified in the query
        /// </summary>
        /// <param name="query">
        /// Query to be evaluated
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// True if at least one record in the specified column is equal to the comparison value, otherwise false.
        /// </returns>
        Task<bool> AnyAsync(Query query, CancellationToken ct = default);

        /// <summary>
        /// Counts the records in the table that meet a condition.
        /// </summary>
        /// <param name="column">
        /// Column to be compared
        /// </param>
        /// <param name="value">
        /// Value to be compared
        /// </param>
        /// <returns>
        /// Number of records that meet the condition
        /// </returns>
        Task<long> CountAsync(string column, object value, CancellationToken ct = default);

        /// <summary>
        /// Counts the records in the table that meet a condition.
        /// </summary>
        /// <param name="column">
        /// Column to be compared
        /// </param>
        /// <param name="op">
        /// Comparison operator
        /// </param>
        /// <param name="value">
        /// Value to be compared
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// Number of records that meet the condition
        /// </returns>
        Task<long> CountAsync(string column, string op, object value, CancellationToken ct = default);

        /// <summary>
        /// Counts the records in the table
        /// </summary>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// Total number of records in the table
        /// </returns>
        Task<long> CountAsync(CancellationToken ct = default);

        /// <summary>
        /// Counts the records in the table that match the query.
        /// </summary>
        /// <param name="query">
        /// Query to be evaluated
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// Number of records that meet the query
        /// </returns>
        Task<long> CountAsync(Query query, CancellationToken ct = default);

        /// <summary>
        /// Creates a new record in the database
        /// </summary>
        /// <param name="store">
        /// Record to be stored
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// The completed task
        /// </returns>
        Task InsertAsync(TModel store, CancellationToken ct = default);

        /// <summary>
        /// Creates a new record in the database and get the Id
        /// </summary>
        /// <param name="store">
        /// Record to be stored
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// The new Id of the record
        /// </returns>
        Task<TId> InsertGetIdAsync(TModel store, CancellationToken ct = default);

        /// <summary>
        /// Executes a multiple insertion
        /// </summary>
        /// <param name="items">
        /// Elements to be saved
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        ///The completed task
        /// </returns>
        Task InsertAsync(IEnumerable<TModel> items, CancellationToken ct = default);

        /// <summary>
        /// Update a database record
        /// </summary>
        /// <param name="update">
        /// Record to update
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        ///The completed task
        /// </returns>
        Task UpdateAsync(TModel update, CancellationToken ct = default);

        /// <summary>
        /// Deletes a database record
        /// </summary>
        /// <param name="id">
        /// Record identifier
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        ///The completed task
        /// </returns>
        Task DestroyAsync(TId id, CancellationToken ct = default);

        /// <summary>
        /// Deletes records containing the specified value in the specified column
        /// </summary>
        /// <param name="column">
        /// Name of the column
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        ///The completed task
        /// </returns>
        Task DestroyAsync(string column, object value, CancellationToken ct = default);

        ///  <summary>
        ///  Deletes records containing the specified value in the specified column
        ///  </summary>
        ///  <param name="column">
        ///  Name of the column
        ///  </param>
        ///  <param name="op">
        /// Comparison operator
        /// </param>
        ///  <param name="value">
        ///  Value to be searched
        ///  </param>
        ///  <param name="ct">
        ///  Cancellation token
        ///  </param>
        ///  <returns>
        /// The completed task
        ///  </returns>
        Task DestroyAsync(string column, string op, object value, CancellationToken ct = default);

        /// <summary>
        /// Deletes all records that meet the specified filters
        /// </summary>
        /// <param name="query">
        /// Query filter
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        ///The completed task
        /// </returns>
        Task DestroyAsync(Query query, CancellationToken ct = default);

        /// <summary>
        /// Retrieves all records in the table
        /// </summary>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// A collection of all the records in the table
        /// </returns>
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(CancellationToken ct = default);

        /// <summary>
        /// Retrieves all records in the database containing in the specified column the specified value.
        /// </summary>
        /// <param name="column">
        /// Column name
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// A collection of all the records in the table
        /// </returns>
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(string column, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves all records in the database containing in the specified column the specified value.
        /// </summary>
        /// <param name="column">
        /// Column name
        /// </param>
        /// <param name="op">
        /// Comparison operator
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// A collection of all the records in the table
        /// </returns>
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(string column, string op, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves all records that match the specified filter.
        /// </summary>
        /// <param name="query">
        /// Query filter
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// A collection of all the records in the table
        /// </returns>
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(Query query, CancellationToken ct = default);

        /// <summary>
        /// Retrieves all records in the table
        /// </summary>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// A collection of all the records in the table
        /// </returns>
        Task<IEnumerable<TModel>> GetAsync(CancellationToken ct = default);

        /// <summary>
        /// Retrieves all records in the database containing in the specified column the specified value.
        /// </summary>
        /// <param name="column">
        /// Column name
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// A collection of all the records in the table
        /// </returns>
        Task<IEnumerable<TModel>> GetAsync(string column, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves all records in the database containing in the specified column the specified value.
        /// </summary>
        /// <param name="column">
        /// Column name
        /// </param>
        /// <param name="op">
        /// Comparison operator
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// A collection of all the records in the table
        /// </returns>
        Task<IEnumerable<TModel>> GetAsync(string column, string op, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves all records that match the specified filter.
        /// </summary>
        /// <param name="query">
        /// Query filter
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// A collection of all the records in the table
        /// </returns>
        Task<IEnumerable<TModel>> GetAsync(Query query, CancellationToken ct = default);

        /// <summary>
        /// Retrieves paged data from the table
        /// </summary>
        /// <param name="page">
        /// Page
        /// </param>
        /// <param name="take">
        /// Maximum number of records to be retrieved
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// A paginated collection of the records in the table
        /// </returns>
        Task<PaginationData<TEntity>> GetPaginationAsync<TEntity>(int page, int take, CancellationToken ct = default);

        /// <summary>
        /// Retrieves paged data from the table
        /// </summary>
        /// <param name="column">
        /// Column name
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="page">
        /// Page
        /// </param>
        /// <param name="take">
        /// Maximum number of records to be retrieved
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// A paginated collection of the records in the table
        /// </returns>
        Task<PaginationData<TEntity>> GetPaginationAsync<TEntity>(string column, object value, int page, int take, CancellationToken ct = default);

        /// <summary>
        /// Retrieves paged data from the table
        /// </summary>
        /// <param name="column">
        /// Column name
        /// </param>
        /// <param name="op">
        /// Comparison operator
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="page">
        /// Page
        /// </param>
        /// <param name="take">
        /// Maximum number of records to be retrieved
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// A paginated collection of the records in the table
        /// </returns>
        Task<PaginationData<TEntity>> GetPaginationAsync<TEntity>(string column, string op, object value, int page, int take, CancellationToken ct = default);

        
        /// <summary>
        /// Returns paged and filtered data
        /// </summary>
        /// <param name="query">
        /// Query filter
        /// </param>
        /// <param name="page">
        /// Page
        /// </param>
        /// <param name="take">
        /// Maximum number of records to be retrieved
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// A paginated collection of the records in the table
        /// </returns>
        Task<PaginationData<TEntity>>
            GetPaginationAsync<TEntity>(Query query, int page, int take, CancellationToken ct = default);

        /// <summary>
        /// Retrieves paged data from the table
        /// </summary>
        /// <param name="page">
        /// Page
        /// </param>
        /// <param name="take">
        /// Maximum number of records to be retrieved
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// A paginated collection of the records in the table
        /// </returns>
        Task<PaginationData<TModel>> GetPaginationAsync(int page, int take, CancellationToken ct = default);
        
        /// <summary>
        /// Retrieves paged data from the table
        /// </summary>
        /// <param name="column">
        /// Column name
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="page">
        /// Page
        /// </param>
        /// <param name="take">
        /// Maximum number of records to be retrieved
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// A paginated collection of the records in the table
        /// </returns>
        Task<PaginationData<TModel>> GetPaginationAsync(string column, object value, int page, int take, CancellationToken ct = default);

        /// <summary>
        /// Retrieves paged data from the table
        /// </summary>
        /// <param name="column">
        /// Column name
        /// </param>
        /// <param name="op">
        /// Comparison operator
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="page">
        /// Page
        /// </param>
        /// <param name="take">
        /// Maximum number of records to be retrieved
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// A paginated collection of the records in the table
        /// </returns>
        Task<PaginationData<TModel>> GetPaginationAsync(string column, string op, object value, int page, int take, CancellationToken ct = default);

        
        /// <summary>
        /// Returns paged and filtered data
        /// </summary>
        /// <param name="query">
        /// Query filter
        /// </param>
        /// <param name="page">
        /// Page
        /// </param>
        /// <param name="take">
        /// Maximum number of records to be retrieved
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// A paginated collection of the records in the table
        /// </returns>
        Task<PaginationData<TModel>>
            GetPaginationAsync(Query query, int page, int take, CancellationToken ct = default);

        /// <summary>
        /// Returns the element that matches the Id.
        /// </summary>
        /// <param name="id">
        /// Identifier to search
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// The record found
        /// </returns>
        Task<TEntity> FirstAsync<TEntity>(TId id, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="column">
        /// Name of the column
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// The record found
        /// </returns>
        Task<TEntity> FirstAsync<TEntity>(string column, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="column">
        /// Name of the column
        /// </param>
        /// <param name="op">
        /// Comparison operator
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// The record found
        /// </returns>
        Task<TEntity> FirstAsync<TEntity>(string column, string op, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="query">
        /// Query filter
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// The record found
        /// </returns>
        Task<TEntity> FirstAsync<TEntity>(Query query, CancellationToken ct = default);

        /// <summary>
        /// Returns the element that matches the Id.
        /// </summary>
        /// <param name="id">
        /// Identifier to search
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// The record found or null
        /// </returns>
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(TId id, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="column">
        /// Name of the column
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// The record found or null
        /// </returns>
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(string column, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="column">
        /// Name of the column
        /// </param>
        /// <param name="op">Comparison operator</param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// The record found or null
        /// </returns>
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(string column, string op, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="query">
        /// Query filter
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of data to be retrieved
        /// </typeparam>
        /// <returns>
        /// The record found or null.
        /// </returns>
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(Query query, CancellationToken ct = default);

        /// <summary>
        /// Returns the element that matches the Id.
        /// </summary>
        /// <param name="id">
        /// Identifier to search
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// The record found
        /// </returns>
        Task<TModel> FirstAsync(TId id, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="column">
        /// Name of the column
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// The record found
        /// </returns>
        Task<TModel> FirstAsync(string column, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="column">
        /// Name of the column
        /// </param>
        /// <param name="op">Comparison operator</param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// The record found
        /// </returns>
        Task<TModel> FirstAsync(string column, string op, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="query">
        /// Query filter
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// The record found
        /// </returns>
        Task<TModel> FirstAsync(Query query, CancellationToken ct = default);

        /// <summary>
        /// Returns the element that matches the Id.
        /// </summary>
        /// <param name="id">
        /// Identifier to search
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// The record found or null
        /// </returns>
        Task<TModel?> FirstOrDefaultAsync(TId id, CancellationToken ct = default);


        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="column">
        /// Name of the column
        /// </param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// The record found or null
        /// </returns>
        Task<TModel?> FirstOrDefaultAsync(string column, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="column">
        /// Name of the column
        /// </param>
        /// <param name="op">Comparison operator</param>
        /// <param name="value">
        /// Value to be searched
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// The record found or null
        /// </returns>
        Task<TModel?> FirstOrDefaultAsync(string column, string op, object value, CancellationToken ct = default);

        /// <summary>
        /// Retrieves the first record matching the filter
        /// </summary>
        /// <param name="query">
        /// Query filter
        /// </param>
        /// <param name="ct">
        /// Cancellation token
        /// </param>
        /// <returns>
        /// The record found or null.
        /// </returns>
        Task<TModel?> FirstOrDefaultAsync(Query query, CancellationToken ct = default);
    }
}