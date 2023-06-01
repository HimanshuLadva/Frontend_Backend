// export interface RegisterUser {
//   email?: string;
//   first_name?: string;
//   last_name?: string;
//   password?: string;
//   password_confirmation?: string;
//   gender?: string;
//   date_of_birth?: string;
// }

export interface UpdatePassword {
  old_password: string;
  new_password: string;
  confirm_new_password: string;
}

export interface RegisterUser {
  id?: number;
  email?: string;
  first_name?: string;
  last_name?: string;
  name?: string;
  gender?: string;
  date_of_birth?: Date;
  phone?: string;
  status?: number;
  group?: Group;
  created_at?: Date;
  updated_at?: Date;
  profile_picture?: string;
  password?: string;
  password_confirmation?: string;
}

export interface Group {
  id?: number;
  name?: string;
  created_at?: null;
  updated_at?: null;
}
