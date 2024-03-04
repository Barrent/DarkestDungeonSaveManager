using System;
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

/// <summary>
/// Player profile view model.
/// </summary>
public class ProfileViewModel : ViewModelBase, IProfileViewModel
{
    /// <summary>
    /// Player profile.
    /// </summary>
    private readonly IProfile _profile;

    /// <summary>
    /// Initializes a new instance of <see cref="ProfileViewModel"/>.
    /// </summary>
    /// <param name="profile">Player profile.</param>
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
        RefreshCommand = new RelayCommand(Refresh);
    }

    /// <summary>
    /// Active save game.
    /// </summary>
    public ISaveGameViewModel ActiveSaveGame { get; }

    /// <summary>
    /// Clears up the backup storage.
    /// </summary>
    public ICommand DeleteAllCommand { get; }

    /// <summary>
    /// Command to delete selected in the UI save games from the backup storage.
    /// </summary>
    public ICommand DeleteCommand { get; }

    /// <summary>
    /// Loads selected save game from the backup storage.
    /// </summary>
    public ICommand LoadCommand { get; }

    /// <summary>
    /// Profile name.
    /// </summary>
    public IParameterViewModel<string> Name { get; }

    /// <summary>
    /// Updates list of available save games.
    /// </summary>
    public ICommand RefreshCommand { get; }

    /// <summary>
    /// Command to save <see cref="IProfileViewModel.ActiveSaveGame"/> to the backup storage.
    /// </summary>
    public ICommand SaveCommand { get; }

    /// <summary>
    /// Backed up save games.
    /// </summary>
    public ObservableCollection<ISaveGameViewModel> Saves { get; }

    /// <summary>
    /// Checks if save game can be backed up.
    /// </summary>
    /// <param name="obj">Command parameter.</param>
    /// <returns> True if can. </returns>
    private bool CanSave(object? obj)
    {
        return true;
    }

    /// <summary>
    /// Deletes selected save games from the backup storage.
    /// </summary>
    /// <param name="obj">Command parameter.</param>
    private void Delete(object? obj)
    {
        var selected = Saves.Where(s => s.IsSelected).ToArray();

        foreach (var saveGameViewModel in selected)
        {
            var model = FindModel(saveGameViewModel);
            _profile.Delete(model);
        }
    }

    /// <summary>
    /// Deletes all the save games from the backup storage.
    /// </summary>
    /// <param name="obj">Command parameter.</param>
    private void DeleteAll(object? obj)
    {
        var saves = _profile.Saves.ToArray();
        foreach (var saveGame in saves)
        {
            _profile.Delete(saveGame);
        }
    }

    /// <summary>
    /// Finds save game model associated with the specified view model.
    /// </summary>
    /// <param name="viewModel">View model.</param>
    /// <returns>Model.</returns>
    private ISaveGame FindModel(ISaveGameViewModel viewModel)
    {
        return _profile.Saves.First(s => string.Equals(s.Path, viewModel.Path.Value, StringComparison.InvariantCultureIgnoreCase));
    }

    /// <summary>
    /// Checks if there is any save game selected.
    /// </summary>
    /// <param name="parameter">Command parameter.</param>
    /// <returns>True if at least 1 save game is selected in the UI.</returns>
    private bool IsAnySelected(object? parameter)
    {
        return Saves.Any(s => s.IsSelected);
    }

    /// <summary>
    /// Checks if a single save game is selected.
    /// </summary>
    /// <param name="parameter">Command parameter.</param>
    /// <returns>True if just 1 save game is selected in the UI.</returns>
    private bool IsSingleSelected(object? parameter)
    {
        return Saves.Count(s => s.IsSelected) == 1;
    }

    /// <summary>
    /// Loads the selected save game from the backup folder to the game folder.
    /// </summary>
    /// <param name="obj">Command parameter.</param>
    private void Load(object? obj)
    {
        var selected = Saves.First(s => s.IsSelected);
        var saveGame = FindModel(selected);
        _profile.Load(saveGame);
    }

    /// <summary>
    /// Handles creation of a save game in the backup storage.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="args">Event args.</param>
    private void OnSaveGameAdded(IProfile sender, SaveGameAddedEventArgs args)
    {
        var saveGame = new SaveGameViewModel(args.SaveGame);
        Saves.Add(saveGame);
    }

    /// <summary>
    /// Handles save game deletion from the backup storage.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="args">Event args.</param>
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

    /// <summary>
    /// Refreshes list of available backups in the UI.
    /// </summary>
    /// <param name="obj">Command parameter.</param>
    private void Refresh(object? obj)
    {
        _profile.Load(_profile.ActiveSave);
    }

    /// <summary>
    /// Copies active save game to the backup storage.
    /// </summary>
    /// <param name="parameter">Command parameter.</param>
    private void Save(object? parameter)
    {
        _profile.Save();
    }
}