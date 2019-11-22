using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Omega.Package;
using UnityEngine;

namespace Omega.Tools.Experimental.UtilitiesAggregator
{
    public sealed class RectTransformUtilities
    {
        /// <summary>
        /// Возвращает всех потомков указанного трансформа, если потомок этого трансформа не кастится к RectTransfrom,
        /// то он не будет добавлен в конечный массив  
        /// </summary>
        /// <param name="root">Трансформ, относительно которого будет осуществляться поиск потомков</param>
        /// <returns>Массив потомков, кроме тех которые не являются производными от RectTransform</returns>
        /// <exception cref="ArgumentNullException">Параметр <param name="root"/>>указывает на null</exception>
        /// <exception cref="MissingReferenceException">Параметр <param name="root"/>>указывает на уничтоженный объект</exception>
        [NotNull]
        public RectTransform[] GetChilds([NotNull] RectTransform root)
        {
            if (root is null)
                throw new ArgumentNullException(nameof(root));
            if (!root)
                throw new MissingReferenceException(nameof(root));

            if (root.childCount == 0)
                return Array.Empty<RectTransform>();

            var result = ListPool<RectTransform>.Rent(root.childCount);

            GetChildsWithoutChecks(root, result);

            var resultArray = result.ToArray();

            return resultArray;
        }

        internal void GetChildsWithoutChecks([NotNull] RectTransform rectTransform,
            [NotNull] List<RectTransform> result)
        {
            var childsCount = rectTransform.childCount;
            for (var i = 0; i < childsCount; i++)
            {
                var child = rectTransform.GetChild(i);
                if (child is RectTransform rectTransformChild)
                    result.Add(rectTransformChild);
            }
        }
    }
}