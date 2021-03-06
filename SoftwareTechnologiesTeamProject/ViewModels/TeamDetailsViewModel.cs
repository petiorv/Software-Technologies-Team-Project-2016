﻿using System.Collections.Generic;

namespace SoftwareTechnologiesTeamProject.ViewModels
{
    using Models;
    using System.Linq;

    public class TeamDetailsViewModel
    {
        public Team Team { get; set; }

        public List<Match> Matches { get; set; }

        public List<Match> HomeGames { get; set; }

        public List<Match> AwayGames { get; set; }

        public League League { get; set; }

        public List<Team> Standings { get; set; }

        public List<Match> GetPlayedMatches()
        {
            return this.Matches.Where(m => m.IsResultUpdated).ToList();
        }

        public List<Match> GetHomePlayedMatches()
        {
            return this.Matches.Where(m => m.HomeTeam.Name == this.Team.Name && m.IsResultUpdated).ToList();
        }

        public List<Match> GetAwayPlayedMatches()
        {
            return this.Matches.Where(m => m.AwayTeam.Name == this.Team.Name && m.IsResultUpdated).ToList();
        }

        public List<Match> GetUpcomingMatches()
        {
            var upcomingMatches = new List<Match>();

            foreach (var match in this.Matches.Where(m => m.IsResultUpdated == false))
            {
                upcomingMatches.Add(match);
            }

            return upcomingMatches.Take(3).ToList();
        }

        public int GetTotalWinsCount()
        {
            return this.GetPlayedMatches().Count(m => m.GetWinnerName() == this.Team.Name);
        }

        public int GetHomeWinsCount()
        {
            return this.GetHomePlayedMatches().Count(m => m.GetWinnerName() == this.Team.Name);
        }

        public int GetAwayWinsCount()
        {
            return this.GetAwayPlayedMatches().Count(m => m.GetWinnerName() != this.Team.Name);
        }

        public List<TeamResultStatsViewModel> GetTeamStats()
        {
            var teamResultStats = new List<TeamResultStatsViewModel>();

            foreach (var match in this.GetPlayedMatches())
            {
                var stat = teamResultStats.FirstOrDefault(r => r.Result == match.Result);
                if (stat != null)
                {
                    stat.MatchesPlayed++;
                }
                else
                {
                    teamResultStats.Add(new TeamResultStatsViewModel
                    {
                        Result = match.Result,
                        MatchesPlayed = 1
                    });
                }
            }

            return teamResultStats.OrderByDescending(s => s.MatchesPlayed).ThenByDescending(s => s.Result).Take(2).ToList();
        }

        public Dictionary<int, Team> GetMiniStandings()
        {
            var resultTeams = new Dictionary<int, Team>();

            for (int i = 0; i < this.Standings.Count; i++)
            {
                var teamName = this.Team.Name;
                var currentTeam = this.Standings[i];

                if (this.Standings.Count == 1)
                {
                    resultTeams.Add(1, this.Standings[0]);
                    return resultTeams;
                }

                if (this.Standings.Count < 3)
                {
                    var miniStandings = this.Standings.OrderByDescending(m => m.Points)
                        .ThenByDescending(m => m.GoalsFor)
                        .ToList();

                    resultTeams.Add(1, miniStandings[0]);
                    resultTeams.Add(2, miniStandings[1]);
                    return resultTeams;
                }

                if (currentTeam.Name == teamName && i == 0)
                {
                    resultTeams.Add(i + 1, this.Standings[i]);
                    resultTeams.Add(i + 2, this.Standings[i + 1]);
                    resultTeams.Add(i + 3, this.Standings[i + 2]);
                    break;
                }

                if (currentTeam.Name == teamName && i == this.Standings.Count - 1)
                {
                    resultTeams.Add(i, this.Standings[i - 2]);
                    resultTeams.Add(i + 1, this.Standings[i - 1]);
                    resultTeams.Add(i + 2, this.Standings[i]);
                    resultTeams.Add(i - 1, this.Standings[i - 2]);
                    resultTeams.Add(i, this.Standings[i - 1]);
                    resultTeams.Add(i + 1, this.Standings[i]);
                    break;
                }

                if (currentTeam.Name == teamName)
                {
                    resultTeams.Add(i, this.Standings[i - 1]);
                    resultTeams.Add(i + 1, this.Standings[i]);
                    resultTeams.Add(i + 2, this.Standings[i + 1]);
                    break;
                }
            }

            return resultTeams;
        }

        public string GetWinsInPercents()
        {
            if (this.Team.GetTotalGamesPlayed() == 0)
            {
                return "0%";
            }

            double totalWins = this.Team.Victories;
            double percents = (totalWins / this.Team.GetTotalGamesPlayed()) * 100;

            return $"{percents:F2}%";
        }

        public string GetDrawsInPercents()
        {
            if (this.Team.GetTotalGamesPlayed() == 0)
            {
                return "0%";
            }

            double totalDraws = this.Team.Draws;
            double percents = (totalDraws / this.Team.GetTotalGamesPlayed()) * 100;

            return $"{percents:F2}%";
        }

        public string GetLossesInPercents()
        {
            if (this.Team.GetTotalGamesPlayed() == 0)
            {
                return "0%";
            }

            double totalLosses = this.Team.Losses;
            double percents = (totalLosses / this.Team.GetTotalGamesPlayed()) * 100;

            return $"{percents:F2}%";
        }

        public string GetMatchesGoalsInfo(string overOrUnder, string numberOfGoals)
        {
            var matches = this.GetPlayedMatches();

            if (overOrUnder == "over")
            {
                if (numberOfGoals == "1.5")
                {
                    int matchesCounter = 0;
                    foreach (var match in matches)
                    {
                        int matchGoals = match.HomeTeamGoals + match.AwayTeamGoals;
                        if (matchGoals > 1)
                        {
                            matchesCounter++;
                        }
                    }

                    if (matchesCounter == 0)
                    {
                        return "0%";
                    }

                    double percents = (matchesCounter / (double)matches.Count) * 100;
                    return $"{percents:F2}%";
                }
                else if (numberOfGoals == "2.5")
                {
                    int matchesCounter = 0;
                    foreach (var match in matches)
                    {
                        int matchGoals = match.HomeTeamGoals + match.AwayTeamGoals;
                        if (matchGoals > 2)
                        {
                            matchesCounter++;
                        }
                    }


                    if (matchesCounter == 0)
                    {
                        return "0%";
                    }

                    double percents = (matchesCounter / (double)matches.Count) * 100;
                    return $"{percents:F2}%";
                }
                else if (numberOfGoals == "3.5")
                {
                    int matchesCounter = 0;
                    foreach (var match in matches)
                    {
                        int matchGoals = match.HomeTeamGoals + match.AwayTeamGoals;
                        if (matchGoals > 3)
                        {
                            matchesCounter++;
                        }
                    }


                    if (matchesCounter == 0)
                    {
                        return "0%";
                    }


                    double percents = (matchesCounter / (double)matches.Count) * 100;
                    return $"{percents:F2}%";
                }
            }
            else if (overOrUnder == "under")
            {
                if (numberOfGoals == "1.5")
                {
                    int matchesCounter = 0;
                    foreach (var match in matches)
                    {
                        int matchGoals = match.HomeTeamGoals + match.AwayTeamGoals;
                        if (matchGoals < 2)
                        {
                            matchesCounter++;
                        }
                    }


                    if (matchesCounter == 0)
                    {
                        return "0%";
                    }

                    double percents = (matchesCounter / (double)matches.Count) * 100;
                    return $"{percents:F2}%";
                }
                else if (numberOfGoals == "2.5")
                {
                    int matchesCounter = 0;
                    foreach (var match in matches)
                    {
                        int matchGoals = match.HomeTeamGoals + match.AwayTeamGoals;
                        if (matchGoals < 3)
                        {
                            matchesCounter++;
                        }
                    }


                    if (matchesCounter == 0)
                    {
                        return "0%";
                    }

                    double percents = (matchesCounter / (double)matches.Count) * 100;
                    return $"{percents:F2}%";
                }
                else if (numberOfGoals == "3.5")
                {
                    int matchesCounter = 0;
                    foreach (var match in matches)
                    {
                        int matchGoals = match.HomeTeamGoals + match.AwayTeamGoals;
                        if (matchGoals < 4)
                        {
                            matchesCounter++;
                        }
                    }

                    if (matchesCounter == 0)
                    {
                        return "0%";
                    }


                    double percents = (matchesCounter / (double)matches.Count) * 100;
                    return $"{percents:F2}%"; ;
                }
            }

            return null;
        }
    }
}