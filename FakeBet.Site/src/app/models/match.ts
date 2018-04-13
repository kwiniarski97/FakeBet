export class Match {
  matchId: string;
  category: string;
  matchTime: Date;
  status: MatchStatus;
  teamAName: string;
  teamAPoints: number;
  teamANationalityCode: string;
  teamBName: string;
  teamBPoints: number;
  teamBNationalityCode: string;
  votes: any[]; // todo change to some vote interface or model
}

export enum MatchStatus {
  NonStarted = 0,
  OnGoing = 1,
  Ended = 2
}
