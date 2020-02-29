#include <iostream>
#include <bitset>
#include <sstream>
#include <cmath>

using namespace std;

int main(int argc, char** argv)
{
	float x1 = atof(argv[1]), x2 = atof(argv[2]);
	if (abs(x1) < abs(x2))	swap(x1, x2);
	cout << "x1 = " << x1 << "\nx2 = " << x2 << endl;
	unsigned int x1_uint = *(unsigned int*) & x1;
	unsigned int x2_uint = *(unsigned int*) & x2;
	cout << "x1 in binary:\n" << bitset<32>(x1_uint) << endl;
	cout << "x2 in binary:\n" << bitset<32>(x2_uint) << endl;

	unsigned int e1 = (x1_uint >> 23)& UINT8_MAX;
	unsigned int m1 = (x1_uint & (UINT32_MAX >> 9)) | (1 << 23);
	bool s1 = x1_uint >> 31;

	unsigned int e2 = (x2_uint >> 23)& UINT8_MAX;
	unsigned int m2 = (x2_uint & (UINT32_MAX >> 9)) | (1 << 23);

	unsigned int e3 = e1;
	bool s3 = s1;
	unsigned int exp_diff = e1 - e2;
	cout << "Exponents difference is: " << exp_diff << endl;
	cout << "M2 before aligning:\n" << bitset<24>(m2) << endl;
	m2 >>= exp_diff;
	cout << "M2 after aligning:\n" << bitset<24>(m2) << endl;
	unsigned int m3;
	cout<<"M1:\n" << bitset<24>(m1) << endl;
	if (s1 == (x2_uint >> 31)) m3 = m1 + m2;
	else m3 = m1 - m2;
	cout << "M1 + M2:\n" << bitset<24>(m3) << endl;

	if (!m3) {
		cout << "M3 = 0, so the result is zero\n";
		s3, e3 = 0;
	}
	else {
		while (!(m3 >> 23 & 1) && m3) {
			cout << "Normalizing result...\n";
			if (m3 >> 24) {
				m3 >>= 1;
				e3 ++;
				cout << "M3 >> 1:\n" << bitset<24>(m3) << "\nE3 = " << bitset<8>(e3) << endl;
			}
			else {
				m3 <<= 1;
				e3 --;
				cout << "M3 << 1:\n" << bitset<24>(m3)	<< "\nE3 = " << bitset<8>(e3) << endl;
			}
		}
		m3 &= (UINT32_MAX >> 9);
	}
	unsigned int res = (s3 << 31) | (e3 << 23) | m3;
	float res_float = *(float*)&res;
	cout << "y = " << res_float << ", in binary = " << bitset<32>(res) << endl;
	
}