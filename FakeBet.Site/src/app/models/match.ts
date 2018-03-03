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
}

enum MatchStatus {
  NonStarted,
  OnGoing,
  Ended
}
