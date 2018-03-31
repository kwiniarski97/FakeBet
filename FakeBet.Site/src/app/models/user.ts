import {Bet} from './bet';
import {UserRoles, UserStatus} from './userenums';

export class User {
  nickName: string;
  email: string;
  createTime: Date;
  points: number;
  bets: Bet[];
  token: string;
  status: UserStatus;
  role: UserRoles;
}
