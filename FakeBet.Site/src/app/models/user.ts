export class UserRegister {
  nickname: string;
  email: string;
  password: string;
}

export enum UserStatus {
  Active,
  NotActivated,
  Deactivated,
  Banned
}
