// <copyright file = "BindableBase.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

public class BindableBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SetAndRaise<T>(ref T original, T value, [CallerMemberName] string propertyName = null!)
    {
        if (original is not null && original.Equals(value))
        {
            return;
        }

        original = value;

        this.OnPropertyChanged(propertyName);
    }

    /// <summary>
    /// Method to set value of property and raise <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="value">New value of property.</param>
    /// <param name="target">Parent of the given property.</param>
    /// <param name="selector">Property to set.</param>
    /// <param name="propertyName">Property name for <see cref="PropertyChanged"/>.</param>
    /// <param name="propertyNames">Another properties to raise <see cref="PropertyChanged"/> on.</param>
    /// <typeparam name="TTarget">Type of the parent of the given property.</typeparam>
    /// <typeparam name="TValue">Type of the property.</typeparam>
    protected void SetAndRaise<TTarget, TValue>(
        TValue value, TTarget target, Expression<Func<TTarget, TValue>> selector,
        [CallerMemberName] string propertyName = null!,
        params string[] propertyNames)
    {
        var expression = (MemberExpression)selector.Body;
        var property = (PropertyInfo)expression.Member;
        property.SetValue(target, value);

        this.OnPropertyChanged(propertyName);

        this.NotifyOthers(propertyNames);
    }

    private void NotifyOthers(params string[] propertyNames)
    {
        if (propertyNames.Length < 1) return;

        foreach (string name in propertyNames)
        {
            this.OnPropertyChanged(name);
        }
    }
}
