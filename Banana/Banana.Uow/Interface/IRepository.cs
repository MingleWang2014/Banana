﻿/***********************************
 * Coder：EminemJK
 * Date：2018-11-16
 **********************************/

using Banana.Uow.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Banana.Uow.Interface
{
    /// <summary>
    /// 仓储接口
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// 插入对象
        /// </summary>
        long Insert(T entity);

        /// <summary>
        /// 更新对象
        /// </summary>
        bool Update(T entity);

        /// <summary>
        /// 删除对象
        /// </summary>
        bool Delete(T entity);

        /// <summary>
        /// 删除全部
        /// </summary>
        bool DeleteAll();

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        int Execute(string sql, dynamic parms = null);


        /// <summary>
        /// 查询对象
        /// </summary>
        T Query(int id);

        /// <summary>
        /// 查询总数
        /// </summary>
        int QueryCount(string whereString = null, object param = null);

        /// <summary>
        /// 查询对象集合
        /// </summary>
        List<T> QueryList(string whereString = null, object param = null);

        /// <summary>
        /// 查询对象集合
        /// </summary>
        Paging<T> QueryList(int pageNum, int pageSize, string whereString = null, object param = null, string order = null, bool asc = false);

        /// <summary>
        /// 表名
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// 开启事务
        /// </summary>
        IDbTransaction OpenTransaction();

        /// <summary>
        /// 事务状态
        /// </summary>
        ETrancationState TrancationState { get; }

        /// <summary>
        /// 对象类型
        /// </summary>
        Type EntityType { get; }
    }
}
