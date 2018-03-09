export class Match {
  matchId: string;
  category: string;
  matchTime: Date;
  matchStatus: MatchStatus;
  teamAName: string;
  teamAPoints: number;
  teamANationalityCode: string;
  teamBName: string;
  teamBPoints: number;
  teamBNationalityCode: string;
  pointsRatio: number;
  votes: any[]; // todo change to some vote interface or model
}

enum MatchStatus {
  NonStarted,
  OnGoing,
  Ended
}
