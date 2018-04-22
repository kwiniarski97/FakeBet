import {UserRoles, UserStatus} from './userenums';

export class User {
  nickName: string;
  email: string;
  createTime: Date;
  points: number;
  status: UserStatus;
  role: UserRoles;
}
