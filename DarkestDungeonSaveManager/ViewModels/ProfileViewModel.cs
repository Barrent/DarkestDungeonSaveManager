using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Barrent.Common.WPF.Commands;
using Barrent.Common.WPF.Interfaces.ViewModels.Parameters;
using Barrent.Common.WPF.ViewModels;
using Barrent.Common.WPF.ViewModels.Parameters;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.ViewModels;
using DarkestDungeonSaveManager.Models;

namespace DarkestDungeonSaveManager.ViewModels;

public class ProfileViewModel : ViewModelBase, IProfileViewModel
{
    private readonly IProfile _profile;

    private ISaveGameViewModel? _selectedSave;

    public ProfileViewModel(IProfile profile)
    {
        _profile = profile;

        Name = new ParameterViewModel<string>(profile.DisplayName);

        ActiveSaveGame = new SaveGameViewModel(profile.ActiveSave);
        
        Saves = new ObservableCollection<ISaveGameViewModel>(_profile.Saves.Select(s => new SaveGameViewModel(s)));

        _profile.SaveGameAdded += OnSaveGameAdded;
        _profile.SaveGameDeleted += OnSaveGameDeleted;

        SaveCommand = new RelayCommand(Save, CanSave);
        DeleteCommand = new RelayCommand(Delete, IsAnySelected);
        DeleteAllCommand = new RelayCommand(DeleteAll, p => Saves.Count > 0);
        LoadCommand = new RelayCommand(Load, IsSingleSelected);
    }

    public ISaveGameViewModel ActiveSaveGame { get; }

    public ICommand DeleteAllCommand { get; }

    public ICommand DeleteCommand { get; }

    public ICommand LoadCommand { get; }

    public IParameterViewModel<string> Name { get; }

    public ICommand SaveCommand { get; }

    public ObservableCollection<ISaveGameViewModel> Saves { get; }

    private bool CanSave(object obj)
    {
        return true;
    }

    private void Delete(object obj)
    {
        var selected = Saves.Where(s => s.IsSelected).ToArray();

        foreach (var saveGameViewModel in selected)
        {
            var model = FindModel(saveGameViewModel);
            if (model != null)
            {
                _profile.Delete(model);
            }
        }
    }

    private ISaveGame? FindModel(ISaveGameViewModel viewModel)
    {
        return _profile.Saves.FirstOrDefault(s => string.Equals(s.Path, viewModel.Path.Value, StringComparison.InvariantCultureIgnoreCase));
    }

    private void DeleteAll(object obj)
    {
        var saves = _profile.Saves.ToArray();
        foreach (var saveGame in saves)
        {
            _profile.Delete(saveGame);
        }
    }

    private bool IsAnySelected(object parameter)
    {
        return Saves.Any(s => s.IsSelected);
    }

    private bool IsSingleSelected(object parameter)
    {
        return Saves.Count(s => s.IsSelected) == 1;
    }

    private void Load(object obj)
    {
        var selected = Saves.First(s => s.IsSelected);
        var saveGame = FindModel(selected);
        _profile.Load(saveGame);
    }

    private void OnSaveGameAdded(IProfile sender, SaveGameAddedEventArgs args)
    {
        var saveGame = new SaveGameViewModel(args.SaveGame);
        Saves.Add(saveGame);
    }

    private void OnSaveGameDeleted(IProfile sender, SaveGameDeletedEventArgs args)
    {
        for (var i = 0; i < Saves.Count; i++)
        {
            if (string.Equals(Saves[i].Path.Value, args.SaveGame.Path, StringComparison.InvariantCultureIgnoreCase))
            {
                Saves.RemoveAt(i);
                i--;
            }
        }
    }
    private void Save(object parameter)
    {
        _profile.Save();
    }

}