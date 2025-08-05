import styled from 'styled-components';

export const PageContainer = styled.div`
  padding: 2rem;
  background-color: #f3f5f7;
  width: 90rem;
  height: 40rem;
`;

export const Container = styled.div`
  max-width: 900px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  gap: 2rem;
`;

export const ProfileHeader = styled.div`
  background-color: white;
  border-radius: 1rem;
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
  padding: 2rem;
  display: flex;
  align-items: center;
  gap: 1.5rem;
`;

export const Avatar = styled.img`
  width: 80px;
  height: 80px;
  border-radius: 50%;
  object-fit: cover;
`;

export const UserInfo = styled.div`
  h2 {
    font-size: 1.75rem;
    font-weight: bold;
    color: #1f2937;
    margin: 0;
  }
  p {
    font-size: 1rem;
    color: #6b7280;
    margin-top: 0.25rem;
  }
`;

export const Card = styled.div`
  background-color: white;
  border-radius: 1rem;
  box-shadow: 0 1px 2px 0 rgb(0 0 0 / 0.05);
  padding: 2rem;
`;

export const SectionTitle = styled.h3`
  font-size: 1.25rem;
  font-weight: 600;
  color: #374151;
  margin: 0 0 1.5rem 0;
  padding-bottom: 1rem;
  border-bottom: 1px solid #e5e7eb;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
`;

export const FormRow = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 1.5rem;
`;

export const FormGroup = styled.div`
  display: flex;
  flex-direction: column;
`;

export const Label = styled.label`
  font-size: 0.875rem;
  font-weight: 500;
  color: #4b5563;
  margin-bottom: 0.5rem;
`;

export const Input = styled.input`
  padding: 0.75rem 1rem;
  border: 1px solid #d1d5db;
  border-radius: 0.5rem;
  font-size: 1rem;
  background-color: #f9fafb;
  transition: all 0.2s ease-in-out;
  color: black;

  &:focus {
    outline: none;
    border-color: #4f46e5;
    box-shadow: 0 0 0 3px rgba(79, 70, 229, 0.1);
  }
`;

export const ButtonContainer = styled.div`
  display: flex;
  justify-content: flex-end;
  margin-top: 1rem;
`;

export const PrimaryButton = styled.button`
  padding: 0.625rem 1.25rem;
  background-color: #4f46e5;
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 0.875rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;

  &:hover {
    background-color: #4338ca;
  }
`;

export const PreferenceRow = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 1rem 0;
    border-bottom: 1px solid #e5e7eb;

    &:last-child {
        border-bottom: none;
    }
`;
