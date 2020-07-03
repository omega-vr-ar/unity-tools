﻿using System;
using JetBrains.Annotations;
using Omega.Package;
using Omega.Tools;
using Omega.Package.Internal;
using UnityEngine;

/// <summary>
/// Класс содержащий расширения для GameObject`ов
/// </summary>
public static class GameObjectExtensions
{
    /// <summary>
    /// Возвращает компонент, прикрепленный к объекту. Если экземпляр компонента заданного типа отсутствует на объекте
    /// то он будет добавлен к объекту. 
    /// </summary>
    /// 
    /// <param name="gameObject">Игровой объект</param>
    /// 
    /// <typeparam name="T">Тип компонента</typeparam>
    /// 
    /// <returns>Экземпляр компонента</returns>
    /// 
    /// <exception cref="ArgumentNullException">Параметр <param name="gameObject"/>>указывает на null</exception>
    /// <exception cref="MissingReferenceException">Параметр <param name="gameObject"/>>указывает на уничтоженный объект</exception>
    [NotNull]
    public static T MissingComponent<T>([NotNull] this GameObject gameObject) where T : Component
    {
        if (gameObject is null)
            throw new NullReferenceException(nameof(gameObject));
        if (!gameObject)
            throw new MissingReferenceException(nameof(gameObject));

        return GameObjectUtilities.MissingComponentWithoutChecks<T>(gameObject);
    }
    
    /// <summary>
    /// Возвращает компонент, прикрепленный к объекту. Если экземпляр компонента заданного типа отсутствует на объекте
    /// то он будет добавлен к объекту. 
    /// </summary>
    /// 
    /// <param name="gameObject">Игровой объект</param>
    /// 
    /// <param name="componentType">Объект-тип компонента</typeparam>
    /// 
    /// <returns>Экземпляр компонента</returns>
    /// 
    /// <exception cref="ArgumentNullException">Параметр <param name="gameObject"/>>указывает на null</exception>
    /// <exception cref="MissingReferenceException">Параметр <param name="gameObject"/>>указывает на уничтоженный объект</exception>
    [NotNull]
    public static Component MissingComponent([NotNull] this GameObject gameObject, Type componentType)
    {
        if (gameObject is null)
            throw new NullReferenceException(nameof(gameObject));
        if (!gameObject)
            throw new MissingReferenceException(nameof(gameObject));

        return GameObjectUtilities.MissingComponentWithoutChecks(gameObject, componentType);
    }

    /// <summary>
    /// Возвращает всех потомков для игрового объекта
    /// </summary>
    /// 
    /// <returns>Массив потомков</returns>
    /// 
    /// <exception cref="NullReferenceException">Параметр <param name="gameObject"/>>указывает на null</exception>
    /// <exception cref="MissingReferenceException">Параметр <param name="gameObject"/>>указывает на уничтоженный объект</exception>
    public static Transform[] GetChildren(this GameObject gameObject)
    {
        if (gameObject is null)
            throw new NullReferenceException(nameof(gameObject));
        if (!gameObject)
            throw new MissingReferenceException(nameof(gameObject));

        return TransformUtilities.GetChildrenWithoutChecks(gameObject.transform);
    }

    [CanBeNull]
    public static T GetComponentInDirectChildren<T>(this GameObject gameObject)
    {
        if (gameObject is null)
            throw new NullReferenceException(nameof(gameObject));
        if (!gameObject)
            throw new MissingReferenceException(nameof(gameObject));

        return GameObjectUtilities.GetComponentDirectWithoutChecks<T>(gameObject);
    }
    
    [NotNull]
    public static T[] GetComponentsInDirectChildren<T>(this GameObject gameObject, bool searchInRoot)
    {
        if (gameObject is null)
            throw new NullReferenceException(nameof(gameObject));
        if (!gameObject)
            throw new MissingReferenceException(nameof(gameObject));

        var result = ListPool<T>.Rent();
        GameObjectUtilities.GetComponentsDirectWithoutChecks(gameObject, result, searchInRoot);

        var resultArray = result.Count == 0 ? Array.Empty<T>() : result.ToArray();
        ListPool<T>.ReturnInternal(result);

        return resultArray;
    }
    
    [Obsolete("Use GetChildren")]
    public static Transform[] GetChilds(this GameObject gameObject) => gameObject.GetChildren();
}
