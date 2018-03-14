import {Bet} from './bet';
import {UserStatus} from './userstatus';

export class User {
  nickName: string;
  email: string;
  createTime: Date;
  points: number;
  bets: Bet[];
  token: string;
  status: UserStatus;
}
