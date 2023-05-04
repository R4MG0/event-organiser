export interface Event {
  id: number;
  title: string
  description: string;
  image: string;
  createdBy?: string;
  createdAt?: string;
  isAuthorizedFor?: string[];
}
