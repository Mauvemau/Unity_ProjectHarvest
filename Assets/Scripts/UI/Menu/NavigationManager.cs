using System;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour {
    [Header("Menus Settings")]
    [Tooltip("A list that holds the different menus in the game")]
    [SerializeField] private List<Menu> menus;
    
    [Header("Index Settings")] 
    [SerializeField, Min(0)] private int currentlySelectedMenu = 0;
    
    [Header("Initialization Settings")] 
    [Tooltip("Whether a menu should be opened on Awake or externally")]
    [SerializeField] private bool startOnAwake = false;
    [Tooltip("This menu will be opened when the program starts. " +
             "(Change the value of Currently Selected Menu to choose another)")]
    [SerializeField, ReadOnly] private Menu openOnBoot;

    [Header("Event Listerers")] 
    [Tooltip("Selects the menu, closes all other menus, and opens the selected menu")]
    [SerializeField] private MenuEventChannelSO onNavigateToMenu;
    [Tooltip("Selects a menu to be opened, doesn't open it")]
    [SerializeField] private MenuEventChannelSO onSelectMenuChannel;
    [Tooltip("Opens a menu that's selected")]
    [SerializeField] private VoidEventChannelSO onOpenSelectedMenuChannel;
    [Tooltip("Closes all open menus")]
    [SerializeField] private VoidEventChannelSO onCloseAllMenusChannel;
    
    private void SelectMenu(Menu menu) {
        for (int i = 0; i < menus.Count; i++) {
            if (menus[i] != menu) continue;
            currentlySelectedMenu = i;
            Debug.Log($"{name}: selected menu \"{menus[i].name}\"");
            return;
        }
        
        Debug.LogError($"{name}: trying to select invalid menu canvas!");
    }

    private void OpenSelectedMenu() {
        if (currentlySelectedMenu >= menus.Count) {
            Debug.LogError($"{name}: {nameof(currentlySelectedMenu)} out of range!");
            return;
        }
        menus[currentlySelectedMenu].Open();
    }
    
    private void CloseAllMenus() {
        foreach (Menu menu in menus) {
            menu.Close();
        }
    }
    
    private void NavigateToMenu(Menu menu) {
        SelectMenu(menu);
        CloseAllMenus();
        OpenSelectedMenu();
    }

    private void Awake() {
        CloseAllMenus();
        if (!startOnAwake) return;
        OpenSelectedMenu();
    }
    
    private void OnValidate() {
        if(menus.Count == 0) return;
        if (currentlySelectedMenu > menus.Count - 1) {
            currentlySelectedMenu = menus.Count - 1;
        }
        
        openOnBoot = menus[currentlySelectedMenu];
    }
    
    private void OnEnable() {
        if (onNavigateToMenu) {
            onNavigateToMenu.OnEventRaised += NavigateToMenu;
        }
        
        if (onSelectMenuChannel) {
            onSelectMenuChannel.OnEventRaised += SelectMenu;
        }
        
        if(onOpenSelectedMenuChannel) {
            onOpenSelectedMenuChannel.OnEventRaised += OpenSelectedMenu;
        }
        
        if (onCloseAllMenusChannel) {
            onCloseAllMenusChannel.OnEventRaised += CloseAllMenus;
        }

        foreach (Menu menu in menus) { // We subscribe externally too.
            if(!menu.onRequestOpenRemotely) continue;
            menu.onRequestOpenRemotely.OnEventRaised += menu.Open;
        }
    }
    
    private void OnDisable() {
        if (onNavigateToMenu) {
            onNavigateToMenu.OnEventRaised -= NavigateToMenu;
        }
        
        if (onSelectMenuChannel) {
            onSelectMenuChannel.OnEventRaised -= SelectMenu;
        }
        
        if(onOpenSelectedMenuChannel) {
            onOpenSelectedMenuChannel.OnEventRaised -= OpenSelectedMenu;
        }
        
        if (onCloseAllMenusChannel) {
            onCloseAllMenusChannel.OnEventRaised -= CloseAllMenus;
        }
        
        foreach (Menu menu in menus) {
            if(!menu.onRequestOpenRemotely) continue;
            menu.onRequestOpenRemotely.OnEventRaised -= menu.Open;
        }
    }
}
