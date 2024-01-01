using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;

namespace FlyFiguresTraineeProject.Figures;

public class FigureCollection : IReadOnlyCollection<MovableFigure>, INotifyCollectionChanged
{
    private readonly ObservableCollection<MovableFigure> _figures;
    private readonly Canvas _canvas;

    public int Count => _figures.Count;
    
    public FigureCollection(Canvas canvas)
    {
        _canvas = canvas;
        _figures = new ObservableCollection<MovableFigure>();
        _figures.CollectionChanged += CollectionChanged;
    }

    public IEnumerator<MovableFigure> GetEnumerator()
    {
        return _figures.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(Func<MovableFigure> factory)
    {
        var figure = factory.Invoke();
        figure.Initialization(_canvas, _figures);
        _figures.Add(figure);
        OnCollectionChanged();
    }

    public void Clear()
    {
        _figures.Clear();
        _canvas.Children.Clear();
        OnCollectionChanged();
    }

    public int IndexOf(MovableFigure figure)
    {
        return _figures.IndexOf(figure);
    }
    
    public void MoveAndDraw()
    {
        foreach (var figure in _figures)
        {
            figure.Move();
            figure.Draw();
        }
    }
    
    public void ChangeFigure(MovableFigure figure)
    {
        var index = _figures.IndexOf(figure);
        _figures.RemoveAt(index);
        _figures.Insert(index, figure);
        OnCollectionChanged();
    }

    public void Restore(IEnumerable<MovableFigureSnapshot> snapshots)
    {
        Clear();
        foreach (var snapshot in snapshots)
            Add(() => snapshot.Restore());
        OnCollectionChanged();
    }

    private void OnCollectionChanged()
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
    
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
}