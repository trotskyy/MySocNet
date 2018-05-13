using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MySocNet.Bll.Dto.Utils;
using MySocNet.Bll.Exceptions;
using MySocNet.Dal.Abstract;

namespace MySocNet.Bll.Services.Abstract
{
    /// <summary>
    /// Any service perfoming select operations from IUnitOfWork
    /// </summary>
    /// <typeparam name="TDtoEntity">DTO entity type</typeparam>
    /// <typeparam name="TEntity">DB entity type</typeparam>
    public abstract class GenericService<TDtoEntity, TEntity>
    {
        protected IUnitOfWorkFactory _unitOfWorkFactory;

        public GenericService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (!AutomapperInitializer.IsAutomapperInited)
                AutomapperInitializer.InitAutoMapper();

            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// ExecuteSelectQuery(unitOfWork => unitOfWork
        ///        .UserRepository
        ///        .GetAllNonSubscriptionsOfMatching(subscriberDb, filter));
        /// </summary>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        protected List<TDtoEntity> ExecuteSelectQuery(Func<IUnitOfWork, List<TEntity>> selectQuery)
        {
            List<TEntity> result;

            try
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.GetUnitOfWork())
                {
                    result = selectQuery.Invoke(unitOfWork);
                }
            }
            catch (Exception ex)
            {
                throw new DomainModelException(ex);
            }

            return Mapper.Map<List<TEntity>, List<TDtoEntity>>(result);
        }

        /// <summary>
        /// ExecuteSelectQuery(unitOfWork => unitOfWork
        ///        .UserRepository
        ///        .GetAllNonSubscriptionsOfMatching(subscriberDb, filter));
        /// </summary>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        protected TDtoEntity ExecuteSelectQuery(Func<IUnitOfWork, TEntity> selectQuery)
        {
            TEntity result;

            try
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.GetUnitOfWork())
                {
                    result = selectQuery.Invoke(unitOfWork);
                }
            }
            catch (Exception ex)
            {
                throw new DomainModelException(ex);
            }

            return Mapper.Map<TEntity, TDtoEntity>(result);
        }

        /// <summary>
        /// ExecuteSelectQuery(unitOfWork => unitOfWork
        ///        .UserRepository
        ///        .GetAllNonSubscriptionsOfMatching(subscriberDb, filter));
        /// </summary>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        protected int ExecuteSelectQuery(Func<IUnitOfWork, int> selectQuery)
        {
            int result;

            try
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.GetUnitOfWork())
                {
                    result = selectQuery.Invoke(unitOfWork);
                }
            }
            catch (Exception ex)
            {
                throw new DomainModelException(ex);
            }

            return result;
        }

        protected void ExecuteNonQuery(Action<IUnitOfWork> query)
        {
            try
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.GetUnitOfWork())
                {
                    query.Invoke(unitOfWork);
                }
            }
            catch (Exception ex)
            {
                throw new DomainModelException(ex);
            }
        }

    }
}
