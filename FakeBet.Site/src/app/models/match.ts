export class Match {
  matchId: string;
  category: string;
  matchTime: Date;
  matchStatus: MatchStatus;
  teamAName: string;
  teamAPoints: number;
  teamBName: string;
  teamBPoints: number;
  pointsRatio: number;
  votes: any[]; // todo change to some vote interface or model
}

enum MatchStatus {
  NonStarted,
  OnGoing,
  Ended
}
