export class Bet {
  betId: number;
  matchId: string;
  userId: string;
  betOnTeamA: number;
  betOnTeamB: number;


  constructor(matchId: string, userId: string, betOnTeamA: number, betOnTeamB: number) {
    this.matchId = matchId;
    this.userId = userId;

    if (!betOnTeamA) {
      this.betOnTeamA = 0;
    } else {
      this.betOnTeamA = betOnTeamA;
    }

    if (!betOnTeamB) {
      this.betOnTeamB = 0;
    } else {
      this.betOnTeamB = betOnTeamB;
    }
  }
}
