// <copyright file = "RelayCommand.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Windows.Input;

namespace DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

public class RelayCommand : ICommand
{
    private readonly Func<bool> _canExecute;
    private readonly Action _execute;

    public RelayCommand(Action execute) : this(execute, () => true)
    {
    }

    public RelayCommand(Action execute, Func<bool> canExecute)
    {
        this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
        this._canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object parameter) => this._canExecute();
    public void Execute(object parameter) => this._execute();
    public void OnCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

public class RelayCommand<T> : ICommand
{
    private readonly Func<T, bool> _canExecute;
    private readonly Action<T> _execute;

    public RelayCommand(Action<T> execute) : this(execute, _ => true)
    {
    }

    public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
    {
        this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
        this._canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object parameter) => this._canExecute((T)parameter);
    public void Execute(object parameter) => this._execute((T)parameter);

    /// <summary>
    ///     Execute without unboxing
    /// </summary>
    /// <param name="parameter">
    ///     Data used by the command. If the command does not require data to be passed, this object can
    ///     be set to null.
    /// </param>
    public void Execute(T parameter) => this._execute(parameter);

    public void OnCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
