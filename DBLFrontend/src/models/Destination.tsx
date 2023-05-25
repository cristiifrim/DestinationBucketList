import { User } from "./User";

export interface Destination {
  id?: number;
  geolocation: string;
  title: string;
  image: string;
  description: string;
  startDate: Date;
  endDate: Date;

  isPublic: boolean;
}
