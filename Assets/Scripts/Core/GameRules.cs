using System.Collections.Generic;
using UnityEngine;

namespace MafiaGame.Core
{
    /// <summary>
    /// Defines all game rules and role definitions for the Mafia Game
    /// </summary>
    public static class GameRules
    {
        // ==================== GAME CONFIGURATION ====================
        
        /// <summary>
        /// Minimum number of players required to start a game
        /// </summary>
        public const int MIN_PLAYERS = 4;
        
        /// <summary>
        /// Maximum number of players allowed in a game
        /// </summary>
        public const int MAX_PLAYERS = 16;
        
        /// <summary>
        /// Duration of the day phase in seconds
        /// </summary>
        public const int DAY_PHASE_DURATION = 120;
        
        /// <summary>
        /// Duration of the night phase in seconds
        /// </summary>
        public const int NIGHT_PHASE_DURATION = 30;
        
        /// <summary>
        /// Duration of discussion phase in seconds
        /// </summary>
        public const int DISCUSSION_DURATION = 60;
        
        /// <summary>
        /// Duration of voting phase in seconds
        /// </summary>
        public const int VOTING_DURATION = 30;

        // ==================== ROLE DEFINITIONS ====================

        /// <summary>
        /// Enumeration of all available roles in the game
        /// </summary>
        public enum Role
        {
            Villager,
            Mafia,
            Doctor,
            Detective,
            Jester,
            SerialKiller,
            Lover,
            Bodyguard,
            Witch,
            Accountant
        }

        /// <summary>
        /// Enumeration of role factions/alignments
        /// </summary>
        public enum Faction
        {
            Village,    // Win by eliminating all Mafia
            Mafia,      // Win by equaling or outnumbering Village
            Neutral     // Special win conditions
        }

        // ==================== ROLE CONFIGURATIONS ====================

        /// <summary>
        /// Stores detailed information about each role
        /// </summary>
        public static readonly Dictionary<Role, RoleConfig> RoleConfigurations = new Dictionary<Role, RoleConfig>
        {
            {
                Role.Villager,
                new RoleConfig
                {
                    Name = "Villager",
                    Faction = Faction.Village,
                    Description = "A regular villager with no special abilities. Vote during the day to eliminate suspects.",
                    CanVoteDuringDay = true,
                    CanVoteEliminate = true,
                    HasNightAbility = false,
                    MaxCount = 999,
                    IsVisible = true
                }
            },
            {
                Role.Mafia,
                new RoleConfig
                {
                    Name = "Mafia",
                    Faction = Faction.Mafia,
                    Description = "Member of the Mafia. Eliminate villagers at night. Know other Mafia members.",
                    CanVoteDuringDay = true,
                    CanVoteEliminate = true,
                    HasNightAbility = true,
                    NightAbilityName = "Kill",
                    MaxCount = 999,
                    IsVisible = false
                }
            },
            {
                Role.Doctor,
                new RoleConfig
                {
                    Name = "Doctor",
                    Faction = Faction.Village,
                    Description = "Protects one player each night from elimination. Cannot protect self.",
                    CanVoteDuringDay = true,
                    CanVoteEliminate = true,
                    HasNightAbility = true,
                    NightAbilityName = "Protect",
                    MaxCount = 1,
                    IsVisible = false
                }
            },
            {
                Role.Detective,
                new RoleConfig
                {
                    Name = "Detective",
                    Faction = Faction.Village,
                    Description = "Investigates one player each night to learn their alignment (Village/Mafia/Neutral).",
                    CanVoteDuringDay = true,
                    CanVoteEliminate = true,
                    HasNightAbility = true,
                    NightAbilityName = "Investigate",
                    MaxCount = 1,
                    IsVisible = false
                }
            },
            {
                Role.Jester,
                new RoleConfig
                {
                    Name = "Jester",
                    Faction = Faction.Neutral,
                    Description = "Wins alone by getting voted out during the day phase.",
                    CanVoteDuringDay = true,
                    CanVoteEliminate = true,
                    HasNightAbility = false,
                    MaxCount = 1,
                    IsVisible = false
                }
            },
            {
                Role.SerialKiller,
                new RoleConfig
                {
                    Name = "Serial Killer",
                    Faction = Faction.Neutral,
                    Description = "Eliminates one player each night. Wins if they are the last player alive.",
                    CanVoteDuringDay = true,
                    CanVoteEliminate = true,
                    HasNightAbility = true,
                    NightAbilityName = "Kill",
                    MaxCount = 1,
                    IsVisible = false
                }
            },
            {
                Role.Lover,
                new RoleConfig
                {
                    Name = "Lover",
                    Faction = Faction.Village,
                    Description = "Paired with another player. If one lover dies, the other dies too.",
                    CanVoteDuringDay = true,
                    CanVoteEliminate = true,
                    HasNightAbility = false,
                    MaxCount = 2,
                    IsVisible = false
                }
            },
            {
                Role.Bodyguard,
                new RoleConfig
                {
                    Name = "Bodyguard",
                    Faction = Faction.Village,
                    Description = "Protects one player each night, blocking their special abilities as well.",
                    CanVoteDuringDay = true,
                    CanVoteEliminate = true,
                    HasNightAbility = true,
                    NightAbilityName = "Guard",
                    MaxCount = 1,
                    IsVisible = false
                }
            },
            {
                Role.Witch,
                new RoleConfig
                {
                    Name = "Witch",
                    Faction = Faction.Village,
                    Description = "Has two potions: one to save and one to kill. Can use during night.",
                    CanVoteDuringDay = true,
                    CanVoteEliminate = true,
                    HasNightAbility = true,
                    NightAbilityName = "Potion",
                    MaxCount = 1,
                    IsVisible = false
                }
            },
            {
                Role.Accountant,
                new RoleConfig
                {
                    Name = "Accountant",
                    Faction = Faction.Village,
                    Description = "Each night, learns how many Mafia members remain alive.",
                    CanVoteDuringDay = true,
                    CanVoteEliminate = true,
                    HasNightAbility = true,
                    NightAbilityName = "Count",
                    MaxCount = 1,
                    IsVisible = false
                }
            }
        };

        // ==================== GAME STATE RULES ====================

        /// <summary>
        /// Checks if the game should end and returns the winner(s)
        /// </summary>
        public static GameEndResult CheckGameEnd(List<Player> players)
        {
            List<Player> villagersAlive = new List<Player>();
            List<Player> mafiaAlive = new List<Player>();
            List<Player> neutralAlive = new List<Player>();

            foreach (Player player in players)
            {
                if (!player.IsAlive) continue;

                RoleConfig roleConfig = GetRoleConfig(player.Role);
                if (roleConfig.Faction == Faction.Village)
                    villagersAlive.Add(player);
                else if (roleConfig.Faction == Faction.Mafia)
                    mafiaAlive.Add(player);
                else
                    neutralAlive.Add(player);
            }

            // Mafia wins if they equal or outnumber villagers
            if (mafiaAlive.Count >= villagersAlive.Count && mafiaAlive.Count > 0)
            {
                return new GameEndResult { HasEnded = true, Winners = Faction.Mafia };
            }

            // Village wins if all Mafia are eliminated
            if (mafiaAlive.Count == 0 && villagersAlive.Count > 0)
            {
                return new GameEndResult { HasEnded = true, Winners = Faction.Village };
            }

            // Check for Jester win condition
            foreach (Player player in neutralAlive)
            {
                if (player.Role == Role.Jester && !player.IsAlive)
                {
                    return new GameEndResult { HasEnded = true, Winners = Faction.Neutral };
                }
            }

            return new GameEndResult { HasEnded = false };
        }

        /// <summary>
        /// Gets the configuration for a specific role
        /// </summary>
        public static RoleConfig GetRoleConfig(Role role)
        {
            return RoleConfigurations.ContainsKey(role) ? RoleConfigurations[role] : null;
        }

        /// <summary>
        /// Determines if a role can perform night actions
        /// </summary>
        public static bool CanPerformNightAction(Role role)
        {
            RoleConfig config = GetRoleConfig(role);
            return config != null && config.HasNightAbility;
        }

        /// <summary>
        /// Gets all roles with a specific faction
        /// </summary>
        public static List<Role> GetRolesByFaction(Faction faction)
        {
            List<Role> roles = new List<Role>();
            foreach (var kvp in RoleConfigurations)
            {
                if (kvp.Value.Faction == faction)
                    roles.Add(kvp.Key);
            }
            return roles;
        }
    }

    // ==================== DATA STRUCTURES ====================

    /// <summary>
    /// Stores configuration details for a specific role
    /// </summary>
    public class RoleConfig
    {
        public string Name;
        public GameRules.Faction Faction;
        public string Description;
        public bool CanVoteDuringDay;
        public bool CanVoteEliminate;
        public bool HasNightAbility;
        public string NightAbilityName;
        public int MaxCount;
        public bool IsVisible;
    }

    /// <summary>
    /// Represents the result of a game end check
    /// </summary>
    public class GameEndResult
    {
        public bool HasEnded;
        public GameRules.Faction Winners;
    }

    /// <summary>
    /// Represents a player in the game
    /// </summary>
    public class Player
    {
        public string Id;
        public string Name;
        public GameRules.Role Role;
        public bool IsAlive = true;
        public bool IsRevealed = false;
    }
}
