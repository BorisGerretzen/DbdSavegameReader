using DbdSavegameReader.Lib.Serializer;

namespace DbdSavegameReader.Test;

public class JsonTests
{
    [TestCase("ExampleSave.json")]
    public void DeserializationHandlesAllProperties(string path)
    {
        var json = File.ReadAllText(path);
        var save = SavegameSerializer.DeserializeJson(json);

        Assert.That(save, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(save.VersionNumber, Is.EqualTo(6));
            Assert.That(save.UserId, Is.EqualTo("1234567890123456"));
            Assert.That(save.SelectedCamper, Is.EqualTo(1));
            Assert.That(save.SelectedSlasher, Is.EqualTo(268435463));
            Assert.That(save.Experience, Is.EqualTo(350000));
            Assert.That(save.BonusExperience, Is.EqualTo(0));
            Assert.That(save.FearTokens, Is.EqualTo(1000));
            Assert.That(save.FirstTimePlaying, Is.False);
            Assert.That(save.CurrentSeasonTicks, Is.EqualTo("12345600000000"));
            Assert.That(save.LastConnectedCharacterIndex, Is.EqualTo(1));
            Assert.That(save.OngoingGameTime, Is.EqualTo("0"));
            Assert.That(save.LastConnectedLoadout.Item, Is.EqualTo("_EMPTY_"));
            Assert.That(save.LastConnectedLoadout.ItemAddOns, Is.EquivalentTo(new[] { "_EMPTY_", "_EMPTY_" }));
            Assert.That(save.LastConnectedLoadout.CamperPerks, Is.EquivalentTo(new[] { "_LOCKED_", "_LOCKED_", "_LOCKED_", "_LOCKED_" }));
            Assert.That(save.LastConnectedLoadout.CamperPerkLevels, Is.EquivalentTo(new[] { 0, 0, 0, 0 }));
            Assert.That(save.LastConnectedLoadout.CamperFavor, Is.EqualTo("_EMPTY_"));
            Assert.That(save.LastConnectedLoadout.Power, Is.EqualTo("_EMPTY_"));
            Assert.That(save.LastConnectedLoadout.PowerAddOns, Is.EquivalentTo(new[] { "_EMPTY_", "_EMPTY_" }));
            Assert.That(save.LastConnectedLoadout.SlasherPerks, Is.EquivalentTo(new[] { "_LOCKED_", "_LOCKED_", "_LOCKED_", "_LOCKED_" }));
            Assert.That(save.LastConnectedLoadout.SlasherPerkLevels, Is.EquivalentTo(new[] { 0, 0, 0, 0 }));
            Assert.That(save.LastConnectedLoadout.SlasherFavor, Is.EqualTo("_EMPTY_"));
            Assert.That(save.CharacterData[0].CharacterDataId, Is.EqualTo(2));
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebLevel, Is.EqualTo(26));
            Assert.That(save.CharacterData[0].CharacterDataValue.PrestigeLevel, Is.EqualTo(0));
            Assert.That(save.CharacterData[0].CharacterDataValue.TimesConfronted, Is.EqualTo(0));
            Assert.That(save.CharacterData[0].CharacterDataValue.PrestigeEarnedDates, Is.Empty);
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebData.VersionNumber, Is.EqualTo(4));
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebData.Level, Is.EqualTo(1));
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebData.RingData[0].NodeData[0].Properties.ContentType, Is.EqualTo("Empty"));
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebData.RingData[0].NodeData[0].Properties.Rarity, Is.EqualTo("UltraRare"));
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebData.RingData[0].NodeData[0].Properties.Tags, Is.Empty);
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebData.RingData[0].NodeData[0].State, Is.EqualTo("Collected"));
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebData.RingData[0].NodeData[0].NodeId, Is.EqualTo("0"));
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebData.RingData[0].NodeData[0].ContentId, Is.EqualTo("None"));
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebData.RingData[0].NodeData[0].BloodWebChest.Id, Is.EqualTo("None"));
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebData.RingData[0].NodeData[0].BloodWebChest.Rarity, Is.EqualTo("Common"));
            Assert.That(save.CharacterData[0].CharacterDataValue.BloodWebData.RingData[0].NodeData[0].BloodWebChest.GivenItemRarity, Is.Empty);
            Assert.That(save.CharacterData[0].CharacterDataValue.CharacterLoadoutData.Item, Is.EqualTo("Item_Camper_Toolbox"));
            Assert.That(save.CharacterData[0].CharacterDataValue.CharacterLoadoutData.ItemAddOns, Is.EquivalentTo(new[] { "Addon_Toolbox_007", "Addon_Toolbox_002" }));
            Assert.That(save.CharacterData[0].CharacterDataValue.CharacterLoadoutData.CamperPerks, Is.EquivalentTo(new[] { "Empathy", "Self_Care", "Sprint_Burst", "Small_Game" }));
            Assert.That(save.CharacterData[0].CharacterDataValue.CharacterLoadoutData.CamperPerkLevels, Is.EquivalentTo(new[] { 2, 2, 2, 2 }));
            Assert.That(save.CharacterData[0].CharacterDataValue.CharacterLoadoutData.CamperFavor, Is.EqualTo("MurkyReagent"));
            Assert.That(save.CharacterData[0].CharacterDataValue.CharacterLoadoutData.Power, Is.EqualTo("None"));
            Assert.That(save.CharacterData[0].CharacterDataValue.CharacterLoadoutData.PowerAddOns, Is.EquivalentTo(new[] { "_EMPTY_", "_EMPTY_" }));
            Assert.That(save.CharacterData[0].CharacterDataValue.CharacterLoadoutData.SlasherPerks, Is.EquivalentTo(new[] { "_LOCKED_", "_LOCKED_", "_LOCKED_", "_LOCKED_" }));
            Assert.That(save.CharacterData[0].CharacterDataValue.CharacterLoadoutData.SlasherPerkLevels, Is.EquivalentTo(new[] { 0, 0, 0, 0 }));
            Assert.That(save.CharacterData[0].CharacterDataValue.CharacterLoadoutData.SlasherFavor, Is.EqualTo("_EMPTY_"));
            Assert.That(save.CharacterData[0].CharacterDataValue.InventoryData[0].Version, Is.EqualTo(16));
            Assert.That(save.CharacterData[0].CharacterDataValue.InventoryData[0].Name, Is.EqualTo("Item_Camper_MedKit02"));
            Assert.That(save.CharacterData[0].CharacterDataValue.InventoryData[1].Version, Is.EqualTo(16));
            Assert.That(save.CharacterData[0].CharacterDataValue.InventoryData[1].Name, Is.EqualTo("Addon_Flashlight_008"));
            Assert.That(save.CharacterData[0].CharacterDataValue.InventoryData[2].Version, Is.EqualTo(16));
            Assert.That(save.CharacterData[0].CharacterDataValue.InventoryData[2].Name, Is.EqualTo("Addon_Toolbox_004"));
            Assert.That(save.CharacterData[0].CharacterDataValue.InventoryData[3].Version, Is.EqualTo(16));
            Assert.That(save.CharacterData[0].CharacterDataValue.InventoryData[3].Name, Is.EqualTo("SweetWilliamSachet"));
        });
    }
}