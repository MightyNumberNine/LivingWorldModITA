﻿using LivingWorldMod.Common.Systems;
using LivingWorldMod.Custom.Enums;
using LivingWorldMod.Custom.Utilities;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace LivingWorldMod.Content.NPCs.Villagers {

    /// <summary>
    /// Base class for all of the villager NPCs in the mod. Has several properties that can be
    /// modified depending on the "personality" of the villagers.
    /// </summary>
    public abstract class Villager : ModNPC {

        /// <summary>
        /// What type of villager this class pertains to. Vital for several functions in the class
        /// and must be defined.
        /// </summary>
        public abstract VillagerType VillagerType {
            get;
        }

        /// <summary>
        /// The current status of the "relationship" between these villagers and the players.
        /// Returns the enum of said status.
        /// </summary>
        public VillagerRelationship RelationshipStatus {
            get {
                int reputation = ReputationSystem.GetVillageReputation(VillagerType);

                if (reputation <= HateThreshold) {
                    return VillagerRelationship.Hate;
                }
                else if (reputation > HateThreshold && reputation <= SevereDislikeThreshold) {
                    return VillagerRelationship.SevereDislike;
                }
                else if (reputation > SevereDislikeThreshold && reputation <= DislikeThreshold) {
                    return VillagerRelationship.Dislike;
                }
                else if (reputation >= LikeThreshold && reputation < LoveThreshold) {
                    return VillagerRelationship.Like;
                }
                else if (reputation >= LoveThreshold) {
                    return VillagerRelationship.Love;
                }

                return VillagerRelationship.Neutral;
            }
        }

        /// <summary>
        /// Threshold that the reputation must cross in order for these villagers to HATE the players.
        /// </summary>
        public virtual int HateThreshold => -95;

        /// <summary>
        /// Threshold that the reputation must cross in order for these villagers to SEVERELY
        /// DISLIKE the players.
        /// </summary>
        public virtual int SevereDislikeThreshold => -45;

        /// <summary>
        /// Threshold that the reputation must cross in order for these villagers to DISLIKE the
        /// players. The villagers will be considered "neutral" towards the players if the
        /// reputation is in-between the Dislike and Like thresholds.
        /// </summary>
        public virtual int DislikeThreshold => -15;

        /// <summary>
        /// Threshold that the reputation must cross in order for these villagers to LIKE the
        /// players. The villagers will be considered "neutral" towards the players if the
        /// reputation is in-between the Dislike and Like thresholds.
        /// </summary>
        public virtual int LikeThreshold => 15;

        /// <summary>
        /// Threshold that the reputation must cross in order for these villagers to LOVE the players.
        /// </summary>
        public virtual int LoveThreshold => 95;

        /// <summary>
        /// Dialogue that is added to the list of reputation dialogue depending on the current
        /// event, if any, that is occurring.
        /// </summary>
        public virtual WeightedRandom<string> EventDialogue {
            get {
                WeightedRandom<string> list = new WeightedRandom<string>();

                list.Add("It is quite an event to see this message... Someone messed up! Contact a Mod Dev!");

                return list;
            }
        }

        /// <summary>
        /// Possible dialogue that these villagers will say when they SEVERELY DISLIKE the players.
        /// </summary>
        public virtual WeightedRandom<string> SevereDislikeDialogue {
            get {
                WeightedRandom<string> list = new WeightedRandom<string>();

                list.Add("I DISLIKE the fact that this is empty... Hurry, contact a Mod Dev!");

                return list;
            }
        }

        /// <summary>
        /// Possible dialogue that these villagers will say when they DISLIKE the players.
        /// </summary>
        public virtual WeightedRandom<string> DislikeDialogue {
            get {
                WeightedRandom<string> list = new WeightedRandom<string>();

                list.Add("I DISLIKE the fact that this is empty... Hurry, contact a Mod Dev!");

                return list;
            }
        }

        /// <summary>
        /// Possible dialogue that these villagers will say when they are NEUTRAL to the players.
        /// </summary>
        public virtual WeightedRandom<string> NeutralDialogue {
            get {
                WeightedRandom<string> list = new WeightedRandom<string>();

                list.Add("I feel NEUTRAL about the fact that this is empty... Hurry, contact a Mod Dev!");

                return list;
            }
        }

        /// <summary>
        /// Possible dialogue that these villagers will say when they LIKE the players.
        /// </summary>
        public virtual WeightedRandom<string> LikeDialogue {
            get {
                WeightedRandom<string> list = new WeightedRandom<string>();

                list.Add("I usually would LIKE the emptiness, but in this case it's not making me happy... Hurry, contact a Mod Dev!");

                return list;
            }
        }

        /// <summary>
        /// Possible dialogue that these villagers will say when they LOVE the players.
        /// </summary>
        public virtual WeightedRandom<string> LoveDialogue {
            get {
                WeightedRandom<string> list = new WeightedRandom<string>();

                list.Add("I usually LOVE emptiness like this, but in this case it's not making me happy... Hurry, contact a Mod Dev!");

                return list;
            }
        }

        public override void SetDefaults() {
            NPC.width = 25;
            NPC.height = 40;
            NPC.friendly = RelationshipStatus != VillagerRelationship.Hate;
            NPC.lifeMax = 500;
            NPC.defense = 15;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 7;
            AnimationType = NPCID.Guide;
        }

        //public override void ActsLikeTownNPC => true;

        public override bool CanChat() => RelationshipStatus != VillagerRelationship.Hate;

        public override string GetChat() {
            WeightedRandom<string> returnedList;

            switch (RelationshipStatus) {
                case VillagerRelationship.Hate:
                    return "..."; //The player will be unable to chat with any villagers if they are hated, but *just in case* they somehow do, make sure to have some kind of dialogue so an error isn't thrown

                case VillagerRelationship.SevereDislike:
                    returnedList = SevereDislikeDialogue;
                    break;

                case VillagerRelationship.Dislike:
                    returnedList = DislikeDialogue;
                    break;

                case VillagerRelationship.Neutral:
                    returnedList = NeutralDialogue;
                    break;

                case VillagerRelationship.Like:
                    returnedList = LikeDialogue;
                    break;

                case VillagerRelationship.Love:
                    returnedList = LoveDialogue;
                    break;

                default:
                    LivingWorldMod.Instance.Logger.Error("Villager Reputation isn't within the normal bounds!");
                    return "Somehow your reputation with us is broken. Contact a mod dev immediately!";
            }

            returnedList.AddList(EventDialogue);

            return returnedList;
        }
    }
}