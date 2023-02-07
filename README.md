





<!-- ABOUT THE PROJECT -->
## About The Project

Test task that calculates all the possible combinations between user-inputed computer parts (from pc-store-inventory.json file).
The possible parts are CPU, Motherboard and Memory. You can choose to enter 1, 2 or all parts and check if they are compatible with one another. If not, the program will throw an exception.

### Sample Input:

Good cases:
Cores:
1)Core - 12900K
2)Core and memory- 12900K, KS16GB
3)Core, Memory, Motherboard - 7950X, SM32GB, MSIX670E

Motherboards:
1)Motherboard - ASX670E
2)Motherboard and CPU - ASX670E, 7950X
3)Motherboard and memory - ASUSB450, 5500
4)Motherboard, CPU and Memory - MSIZ590, 11700K, GS8GB

Memory:
1)Memory - KS16GB
2)Memory and Motherboard - CR8GB, 5500
3)Memory, Motherboard and CPU - KS16GB, 7950X, MSIX670E


Bad cases:
Cores:
1) Core - 129000
2) Core and memory - 5500, KS16GB
3) Core and motherboard - 5600X, MSIZ590 

Motherboard:
1) Core, memory and motherboard - MSIZ590, 5500, KS16GB
2) Motherboard and CPU - ASX670E, 12500

Memory:
1) Memory and CPU - KS16GB, 5500
2) Memory, CPU and Motherboard - KS16GB, 5500, ASUSZ690

### Raw input

Good inputs:
12900K
12900K, KS16GB
7950X, SM32GB, MSIX670E
ASX670E
ASX670E, 7950X
ASUSB450, 5500
MSIZ590, 11700K, GS8GB
KS16GB
CR8GB, 5500
KS16GB, 7950X, MSIX670E


Bad inputs:
129000
5500, KS16GB
5600X, MSIZ590 
MSIZ590, 5500, KS16GB
ASX670E, 12500
KS16GB, 5500
KS16GB, 5500, ASUSZ690
KS16GB, ASUSZ690
KS16GB, GBB560M
MSIX570, GS8GB



### Installation

Setup to run the project: 
1. Clone the repo
```sh
git clone https://gitlab.com/team-gsg/olzo.git
```
2. Run the project


## See also

My projects from Telerik Academy Alpha:

https://gitlab.com/users/GeorgiGeorgiev93/contributed




<!-- CONTACT -->
## Contact

Georgi Georgiev - georgi_georgiev_93@abv.bg      https://www.linkedin.com/in/georgi-georgiev-94151a259/ 

Project Link: (https://gitlab.com/team-gsg/olzo/-/tree/master)





<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/othneildrew/Best-README-Template.svg?style=flat-square
[contributors-url]: https://github.com/othneildrew/Best-README-Template/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/othneildrew/Best-README-Template.svg?style=flat-square
[forks-url]: https://github.com/othneildrew/Best-README-Template/network/members
[stars-shield]: https://img.shields.io/github/stars/othneildrew/Best-README-Template.svg?style=flat-square
[stars-url]: https://github.com/othneildrew/Best-README-Template/stargazers
[issues-shield]: https://img.shields.io/github/issues/othneildrew/Best-README-Template.svg?style=flat-square
[issues-url]: https://github.com/othneildrew/Best-README-Template/issues
[license-shield]: https://img.shields.io/github/license/othneildrew/Best-README-Template.svg?style=flat-square
[license-url]: https://github.com/othneildrew/Best-README-Template/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/othneildrew
[product-screenshot]: https://ibb.co/3p2h2Kb
[update-data-screenshot]: https://ibb.co/3p2h2Kb
