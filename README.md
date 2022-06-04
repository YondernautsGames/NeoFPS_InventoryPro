# NeoFPS_InventoryPro
NeoFPS and Inventory Pro integration assets

**IMPORTANT NOTE: At the start of October '19, DevDog announced that they were open-sourcing Inventory Pro along with a number of other assets. You can read more about the decision [here](https://devdog.io/blog/were-open-sourcing-lots-of-our-tools/?utm_source=sendinblue&utm_campaign=OpenSourcing_our_tools!&utm_medium=email). This also means that they will not be actively supporting Inventory Pro.**

**Therefore, this integration is provided as is and will not be actively developed on by Yondernauts Games.**

If the community continues to develop and build on Inventory Pro, updating it to maintain compatibility with future versions of Unity, then this integration will be updated to prevent incompatibility with NeoFPS updates.

## Requirements
This repository was created using Unity 2018.1

It requires the assets [NeoFPS](https://assetstore.unity.com/packages/templates/systems/neofps-150179?aid=1011l58Ft) and [Inventory Pro](https://github.com/devdogio/Inventory-Pro) (deprecated from the asset store).

## Installation
This integration example is intended to be dropped in to a fresh project along with NeoFPS and Inventory Pro.

1. Import NeoFPS and apply the required Unity settings using the NeoFPS Settings Wizard. You can find more information about this process [here](https://docs.neofps.com/manual/neofps-installation.html).

2. Import the Inventory Pro repository.

3. Clone this repository to a folder inside the project Assets folder such as "NeoFPS_InventoryPro"

4. Open the demo scene at *NeoFPS_InventoryPro/Demo Scene*

## Features
- Grid based character inventory
- Drag and drop hot-bar for weapons and items
- NeoFPS weapons set up as Inventory Pro items
- Loot containers
- Death loot drops (including player inventory)
- Vendors
- Player stats for health, speed and strength, along with items to modify them
	
## Integration
The following are the important assets in this repo that enable NeoFPS and Emerald AI to work side by side.

## Known Issues
- The vendor causes an error when accessing. This can be worked around with a null check `storedItem.itemCollection?[storedItem.index].Repaint();` at the location the error points to, but I haven't dug deep into what causes it in the first place.
